// <copyright file="RelaysService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using ZwemTools.Data.TeamManager;
using ZwemTools.Resources.Languages;

namespace ZwemTools;

/// <summary>
/// Calculates optimal relay teams.
/// </summary>
public class RelaysService
{
    private readonly ITeamManagerDatabase database;
    private readonly IStringLocalizer<Strings> localizer;
    private readonly IMemoryCache swimStylesCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelaysService"/> class.
    /// </summary>
    /// <param name="database">The Team Manager database.</param>
    /// <param name="localizer">The string localizer.</param>
    public RelaysService(ITeamManagerDatabase database, IStringLocalizer<Strings> localizer)
    {
        this.database = database;
        this.localizer = localizer;
        this.swimStylesCache = new MemoryCache(new MemoryCacheOptions());
    }

    /// <summary>
    /// Generates relays for the given event.
    /// </summary>
    /// <param name="meet">The meet.</param>
    /// <param name="ev">The event in the meet to generate the relays for.</param>
    /// <param name="availableMembers">The members to consider.</param>
    /// <param name="count">The amount of relays to generate.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IEnumerable<Relay>> CalculateRelays(Meet meet, Event ev, ICollection<Member> availableMembers, int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, this.localizer["The amount of relays must not be negative."]);
        }

        ICollection<Relay> relays = new List<Relay>();
        for (int i = 0; i < count; i++)
        {
            relays.Add(await this.CalculateRelay(meet, ev, availableMembers.Except(relays.SelectMany(r => r.Positions.Select(p => p.Member).WhereNotNull())).ToList()));
        }

        return relays;
    }

    private Task<Relay> CalculateRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        if (ev.SwimStyle is null || ev.SwimStyle.RelayCount <= 1)
        {
            throw new ArgumentException(this.localizer["The event must be a relay event."], nameof(ev));
        }

        if (availableMembers.Count < ev.SwimStyle.RelayCount)
        {
            throw new ArgumentException(this.localizer["The amount of available swimmers must be at least the amount of relay positions."], nameof(availableMembers));
        }

        if (ev.SwimStyle.Stroke == Stroke.Medley)
        {
            return this.CalculateMedleyRelay(meet, ev, availableMembers);
        }

        return this.CalculateSingleStrokeRelay(meet, ev, availableMembers);
    }

    private async Task<Relay> CalculateMedleyRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

        if (ev.SwimStyle.RelayCount == 4)
        {
            List<Relay> relayOptions = await new[] { Stroke.Fly, Stroke.Back, Stroke.Breast, Stroke.Free }
                .Permute()
                .ToAsyncEnumerable()
                .SelectManyAwait(
                    async strokes =>
                    {
                        if (ev.Gender == Gender.Mixed)
                        {
                            int membersPerGender = ev.SwimStyle.RelayCount / 2;
                            return Enumerable.Repeat(Gender.Male, membersPerGender).Concat(Enumerable.Repeat(Gender.Female, membersPerGender))
                                .Permute()
                                .Select(strokes.Zip)
                                .ToAsyncEnumerable()
                                .SelectAwait(async tuples => await this.GetRelay(meet, ev, availableMembers, tuples));
                        }

                        return new[] { await this.GetRelay(meet, ev, availableMembers, strokes.Select(stroke => (stroke, ev.Gender))) }.ToAsyncEnumerable();
                    })
                .ToListAsync();

            return relayOptions.MinBy(relay => relay.EntryTime)!;
        }

        throw new NotImplementedException(this.localizer["This type of relay is not supported."]);
    }

    private async Task<Relay> GetRelay(Meet meet, Event ev, ICollection<Member> availableMembers, IEnumerable<(Stroke Stroke, Gender Gender)> strokes)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

        Dictionary<Stroke, (Member Member, TimeSpan EntryTime)> members = new();
        foreach ((Stroke stroke, Gender gender) in strokes)
        {
            SwimStyle swimStyle = await this.GetSwimStyle(ev.SwimStyle.Distance, stroke);
            (Member Member, TimeSpan EntryTime) fastestForStroke = (await this.database.GetFastestMembers(
                swimStyle,
                gender,
                ev.MinAge,
                ev.MaxAge,
                meet.AgeDate,
                availableMembers.ExceptBy(members.Select(x => x.Value.Member.Id), member => member.Id))).FirstOrDefault();
            members[stroke] = fastestForStroke;
        }

        RelayPosition CreateRelayPosition(Stroke stroke, int number)
        {
            (Member Member, TimeSpan EntryTime) m = members[stroke];
            return new RelayPosition
            {
                RelayId = -1,
                Number = number,
                EntryTime = (int)Math.Round(m.EntryTime.TotalMilliseconds),
                Member = m.Member,
                Stroke = stroke,
            };
        }

        ICollection<RelayPosition> relayPositions = new List<RelayPosition>
        {
            CreateRelayPosition(Stroke.Back, 1),
            CreateRelayPosition(Stroke.Breast, 2),
            CreateRelayPosition(Stroke.Fly, 3),
            CreateRelayPosition(Stroke.Free, 4),
        };

        return new Relay
        {
            Id = -1,
            Positions = relayPositions,
        };
    }

    private async Task<Relay> CalculateSingleStrokeRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

        SwimStyle swimStyle = await this.GetSwimStyle(ev.SwimStyle.Distance, ev.SwimStyle.Stroke);

        if (ev.Gender != Gender.Mixed)
        {
            IEnumerable<(Member Member, TimeSpan EntryTime)> members =
                (await this.database.GetFastestMembers(swimStyle, ev.Gender, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers)).Take(
                    ev.SwimStyle.RelayCount);
            return new Relay
            {
                Id = -1,
                Positions = members.Select((x, i) => new RelayPosition
                {
                    RelayId = -1,
                    Number = i + 1,
                    Member = x.Member,
                    EntryTime = (int)Math.Round(x.EntryTime.TotalMilliseconds),
                    Stroke = ev.SwimStyle.Stroke,
                }).ToList(),
            };
        }

        IEnumerable<(Member Member, TimeSpan EntryTime)> males =
            (await this.database.GetFastestMembers(swimStyle, Gender.Male, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers)).Take(
                ev.SwimStyle.RelayCount / 2);
        IEnumerable<(Member Member, TimeSpan EntryTime)> females =
            (await this.database.GetFastestMembers(swimStyle, Gender.Female, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers)).Take(
                ev.SwimStyle.RelayCount / 2);
        return new Relay
        {
            Id = -1,
            Positions = males.Zip(females).SelectMany(tuple => new[] { tuple.First, tuple.Second }).Select((x, i) => new RelayPosition
            {
                RelayId = -1,
                Number = i + 1,
                Member = x.Member,
                EntryTime = (int)Math.Round(x.EntryTime.TotalMilliseconds),
                Stroke = ev.SwimStyle.Stroke,
            }).ToList(),
        };
    }

    private async Task<SwimStyle> GetSwimStyle(int distance, Stroke stroke)
    {
        return await this.swimStylesCache.GetOrCreateAsync(distance + stroke.ToString(), async _ => await this.database.GetSwimStyle(distance, stroke))
               ?? throw new InvalidOperationException($"Swim style not found. {distance}m {stroke.ToString()}");
    }
}

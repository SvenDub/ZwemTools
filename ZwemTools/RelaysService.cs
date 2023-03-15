// <copyright file="RelaysService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Diagnostics;
using Microsoft.Extensions.Localization;
using ZwemTools.Data.TeamManager;
using ZwemTools.Resources.Languages;

namespace ZwemTools;

public class RelaysService
{
    private readonly ITeamManagerDatabase database;
    private readonly IStringLocalizer<Strings> localizer;

    public RelaysService(ITeamManagerDatabase database, IStringLocalizer<Strings> localizer)
    {
        this.database = database;
        this.localizer = localizer;
    }

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

    public Task<Relay> CalculateRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
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

        if (ev.SwimStyle.RelayCount == 4 && ev.Gender != Gender.Mixed)
        {
            List<Relay> relayOptions = await new[] { Stroke.Fly, Stroke.Back, Stroke.Breast, Stroke.Free }
                .Permute()
                .ToAsyncEnumerable()
                .SelectAwait(async strokes =>
                {
                    Dictionary<Stroke, (Member Member, TimeSpan EntryTime)> members = new();
                    foreach (Stroke stroke in strokes)
                    {
                        (Member Member, TimeSpan EntryTime) fastestForStroke = (await this.database.GetFastestMembers(ev.SwimStyle.Distance,
                            stroke,
                            ev.Gender,
                            ev.MinAge,
                            ev.MaxAge,
                            meet.AgeDate,
                            availableMembers.Except(members.Select(x => x.Value.Member)))).FirstOrDefault();
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
                }).ToListAsync();

            return relayOptions.MinBy(relay => relay.EntryTime)!;
        }

        throw new NotImplementedException(this.localizer["This type of relay is not supported."]);
    }

    private async Task<Relay> CalculateSingleStrokeRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");
        if (ev.Gender != Gender.Mixed)
        {
            IEnumerable<(Member Member, TimeSpan EntryTime)> members =
                (await this.database.GetFastestMembers(ev.SwimStyle.Distance, ev.SwimStyle.Stroke, ev.Gender, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers)).Take(
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

        throw new NotImplementedException(this.localizer["This type of relay is not supported."]);
    }
}

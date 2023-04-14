// <copyright file="RelaysService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Components.Web;
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
    /// <param name="minimumEntryDate">The minimum date from which to search for entry times.</param>
    /// <param name="progressEventHandler">Event handler called when the progress is updated.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IEnumerable<Relay>> CalculateRelays(Meet meet, Event ev, ICollection<Member> availableMembers, int count, DateTime minimumEntryDate, EventHandler<ProgressEventArgs>? progressEventHandler = null, CancellationToken cancellationToken = default)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, this.localizer["The amount of relays must not be negative."]);
        }

        RelayCalculation relayCalculation = new(this.database, this.localizer, this.swimStylesCache, progressEventHandler);
        return await relayCalculation.CalculateRelays(meet, ev, availableMembers, count, minimumEntryDate, cancellationToken);
    }

    private sealed class RelayCalculation
    {
        private readonly ITeamManagerDatabase database;
        private readonly IStringLocalizer<Strings> localizer;
        private readonly IMemoryCache swimStylesCache;
        private readonly EventHandler<ProgressEventArgs>? progressEventHandler;

        private int currentRelay;
        private int maxRelays;

        private int currentOption;
        private int maxOptions;

        public RelayCalculation(ITeamManagerDatabase database, IStringLocalizer<Strings> localizer, IMemoryCache swimStylesCache, EventHandler<ProgressEventArgs>? progressEventHandler = null)
        {
            this.database = database;
            this.localizer = localizer;
            this.swimStylesCache = swimStylesCache;
            this.progressEventHandler = progressEventHandler;
        }

        public async Task<IEnumerable<Relay>> CalculateRelays(
            Meet meet,
            Event ev,
            ICollection<Member> availableMembers,
            int count,
            DateTime minimumEntryDate,
            CancellationToken cancellationToken = default)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, this.localizer["The amount of relays must not be negative."]);
            }

            this.maxRelays = count;

            ICollection<Member> membersOfAge = availableMembers.Where(member => HasCorrectAge(member, ev, meet)).ToList();

            ICollection<Relay> relays = new List<Relay>();
            for (; this.currentRelay < this.maxRelays; this.currentRelay++)
            {
                relays.Add(
                    await this.CalculateRelay(
                        ev,
                        membersOfAge.ExceptBy(relays.SelectMany(r => r.Positions.Select(p => p.Member?.Id).WhereNotNull()), member => member.Id).ToList(),
                        minimumEntryDate,
                        cancellationToken));
                this.ReportProgress();
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }

            return relays;
        }

        private static bool HasCorrectAge(Member member, Event ev, Meet meet)
        {
            (DateTime minDate, DateTime maxDate) = GetAgeRange(ev.MinAge, ev.MaxAge, meet.AgeDate, meet.AgeCalcType);
            return member.Birthdate >= minDate && member.Birthdate <= maxDate;
        }

        private static (DateTime MinDate, DateTime MaxDate) GetAgeRange(int minAge, int maxAge, DateTime ageDate, AgeCalcType ageCalcType)
        {
            if (ageCalcType == AgeCalcType.Date)
            {
                return (
                    MinDate: ageDate.AddYears(-maxAge),
                    MaxDate: ageDate.AddYears(-minAge));
            }

            if (ageCalcType == AgeCalcType.Year)
            {
                return (
                    MinDate: new DateTime(ageDate.Year - maxAge, 1, 1),
                    MaxDate: new DateTime(ageDate.Year - minAge + 1, 1, 1));
            }

            if (ageCalcType == AgeCalcType.CanFnq)
            {
                DateTime ageDateInSeason = AgeDateInSeason(ageDate);
                return (
                    MinDate: ageDateInSeason.AddYears(-maxAge),
                    MaxDate: ageDateInSeason.AddYears(-minAge));
            }

            throw new NotImplementedException($"Age calculation method {ageCalcType} not supported");
        }

        private static DateTime AgeDateInSeason(DateTime dateTime)
        {
            if (dateTime >= new DateTime(dateTime.Year, 9, 1))
            {
                // Date is after september 1st
                return new DateTime(dateTime.Year, 12, 31);
            }

            return new DateTime(dateTime.Year - 1, 12, 31);
        }

        private static T Factorial<T>(T n)
            where T : INumber<T>
        {
            if (n <= T.One)
            {
                return n;
            }

            return n * Factorial(n - T.One);
        }

        private void ReportProgress()
        {
            this.progressEventHandler?.Invoke(this, new ProgressEventArgs { Loaded = (this.maxOptions * this.currentRelay) + this.currentOption, Total = this.maxRelays * this.maxOptions, LengthComputable = true });
        }

        private Task<Relay> CalculateRelay(Event ev, ICollection<Member> availableMembers, DateTime minimumEntryDate, CancellationToken cancellationToken = default)
        {
            if (ev.SwimStyle is null || ev.SwimStyle.RelayCount <= 1)
            {
                throw new ArgumentException(this.localizer["The event must be a relay event."], nameof(ev));
            }

            if (ev.SwimStyle.Stroke == Stroke.Medley)
            {
                return this.CalculateMedleyRelay(ev, availableMembers, minimumEntryDate, cancellationToken);
            }

            return this.CalculateSingleStrokeRelay(ev, availableMembers, minimumEntryDate);
        }

        private async Task<Relay> CalculateMedleyRelay(
            Event ev,
            ICollection<Member> availableMembers,
            DateTime minimumEntryDate,
            CancellationToken cancellationToken = default)
        {
            Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

            if (ev.SwimStyle.RelayCount == 4)
            {
                this.currentOption = 0;
                this.maxOptions = Factorial(ev.SwimStyle.RelayCount);
                if (ev.Gender == Gender.Mixed)
                {
                    this.maxOptions *= Factorial(ev.SwimStyle.RelayCount - 1);
                }

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
                                    .DistinctBy(genders => genders, new EnumerableEqualityComparer<Gender>())
                                    .Select(strokes.Zip)
                                    .ToAsyncEnumerable()
                                    .SelectAwait(
                                        async tuples =>
                                        {
                                            Relay relay = await this.GetRelay(ev, availableMembers, tuples, minimumEntryDate);
                                            this.ReportProgress();
                                            await Task.Yield();
                                            return relay;
                                        });
                            }

                            Relay relay = await this.GetRelay(ev, availableMembers, strokes.Select(stroke => (stroke, ev.Gender)), minimumEntryDate);
                            this.ReportProgress();
                            await Task.Yield();
                            return new[] { relay }
                                .ToAsyncEnumerable();
                        })
                    .ToListAsync(cancellationToken);

                return relayOptions.Where(relay => relay.IsComplete).MinBy(relay => relay.EntryTime)!;
            }

            throw new NotImplementedException(this.localizer["This type of relay is not supported."]);
        }

        private async Task<Relay> GetRelay(
            Event ev,
            ICollection<Member> availableMembers,
            IEnumerable<(Stroke Stroke, Gender Gender)> strokes,
            DateTime minimumEntryDate)
        {
            Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");
            this.currentOption++;

            Dictionary<Stroke, (Member Member, TimeSpan EntryTime)> members = new();
            foreach ((Stroke stroke, Gender gender) in strokes)
            {
                SwimStyle swimStyle = await this.GetSwimStyle(ev.SwimStyle.Distance, stroke);
                (Member Member, TimeSpan EntryTime) fastestForStroke = (await this.database.GetFastestMembers(
                    swimStyle,
                    gender,
                    availableMembers.ExceptBy(members.Select(x => x.Value.Member.Id), member => member.Id),
                    minimumEntryDate)).FirstOrDefault();
                if (fastestForStroke.Member is not null)
                {
                    members[stroke] = fastestForStroke;
                }

                // Yield control to prevent blocking the UI thread for a long time
                await Task.Yield();
            }

            RelayPosition CreateRelayPosition(Stroke stroke, int number)
            {
                if (members.TryGetValue(stroke, out (Member Member, TimeSpan EntryTime) m))
                {
                    return new RelayPosition
                    {
                        RelayId = -1,
                        Number = number,
                        EntryTime = (int)Math.Round(m.EntryTime.TotalMilliseconds),
                        Member = m.Member,
                        Stroke = stroke,
                    };
                }

                return new RelayPosition
                {
                    RelayId = -1,
                    Number = number,
                    EntryTime = 0,
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

        private async Task<Relay> CalculateSingleStrokeRelay(Event ev, ICollection<Member> availableMembers, DateTime minimumEntryDate)
        {
            Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

            SwimStyle swimStyle = await this.GetSwimStyle(ev.SwimStyle.Distance, ev.SwimStyle.Stroke);

            if (ev.Gender != Gender.Mixed)
            {
                IEnumerable<(Member Member, TimeSpan EntryTime)> members =
                    (await this.database.GetFastestMembers(swimStyle, ev.Gender, availableMembers, minimumEntryDate))
                    .Take(ev.SwimStyle.RelayCount)
                    .AtLeast(ev.SwimStyle.RelayCount);
                return new Relay
                {
                    Id = -1,
                    Positions = members.Select(
                        (x, i) => new RelayPosition
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
                (await this.database.GetFastestMembers(swimStyle, Gender.Male, availableMembers, minimumEntryDate))
                .Take(ev.SwimStyle.RelayCount / 2)
                .AtLeast(ev.SwimStyle.RelayCount / 2);
            IEnumerable<(Member Member, TimeSpan EntryTime)> females =
                (await this.database.GetFastestMembers(swimStyle, Gender.Female, availableMembers, minimumEntryDate))
                .Take(ev.SwimStyle.RelayCount / 2)
                .AtLeast(ev.SwimStyle.RelayCount / 2);
            return new Relay
            {
                Id = -1,
                Positions = males.Zip(females).SelectMany(tuple => new[] { tuple.First, tuple.Second }).Select(
                    (x, i) => new RelayPosition
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
}

using Microsoft.Extensions.Localization;
using Schotejil.Clubkampioen.Data.TeamManager;
using Schotejil.Clubkampioen.Resources.Languages;
using System.Diagnostics;

namespace Schotejil.Clubkampioen;
public class RelaysService
{
    private readonly ITeamManagerDatabase database;
    private readonly IStringLocalizer<Strings> localizer;

    public RelaysService(ITeamManagerDatabase database, IStringLocalizer<Strings> localizer)
    {
        this.database = database;
        this.localizer = localizer;
    }

    public Relay CalculateRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
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

    private Relay CalculateMedleyRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");

        if (ev.SwimStyle.RelayCount == 4 && ev.Gender != Gender.Mixed)
        {
            var relayOptions = new[] { Stroke.Fly, Stroke.Back, Stroke.Breast, Stroke.Free }
            .Permute()
            .Select(strokes =>
            {
                Dictionary<Stroke, (Member Member, TimeSpan EntryTime)> members = new();
                foreach (var stroke in strokes)
                {
                    var fastestForStroke = this.database.GetFastestMembers(ev.SwimStyle.Distance, stroke, ev.Gender, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers.Except(members.Select(x => x.Value.Member))).FirstOrDefault();
                    members[stroke] = fastestForStroke;
                }

                RelayPosition CreateRelayPosition(Stroke stroke, int number)
                {
                    (Member Member, TimeSpan EntryTime) m = members[stroke];
                    return new RelayPosition()
                    {
                        RelayId = -1,
                        Number = number,
                        EntryTime = (int)Math.Round(m.EntryTime.TotalMilliseconds),
                        Member = m.Member,
                        Stroke = stroke,
                    };
                }

                ICollection<RelayPosition> relayPositions = new List<RelayPosition>() {
                    CreateRelayPosition(Stroke.Back, 1),
                    CreateRelayPosition(Stroke.Breast, 2),
                    CreateRelayPosition(Stroke.Fly, 3),
                    CreateRelayPosition(Stroke.Free, 4),
                };

                return new Relay()
                {
                    Id = -1,
                    Positions = relayPositions,
                };
            }).ToList();

            return relayOptions.MinBy(relay => relay.EntryTime)!;
        }

        throw new NotImplementedException(this.localizer["This type of relay is not supported."]);
    }

    private Relay CalculateSingleStrokeRelay(Meet meet, Event ev, ICollection<Member> availableMembers)
    {
        Debug.Assert(ev.SwimStyle != null, "ev.SwimStyle != null");
        if (ev.Gender != Gender.Mixed)
        {
            IEnumerable<(Member Member, TimeSpan EntryTime)> members = this.database.GetFastestMembers(ev.SwimStyle.Distance, ev.SwimStyle.Stroke, ev.Gender, ev.MinAge, ev.MaxAge, meet.AgeDate, availableMembers).Take(ev.SwimStyle.RelayCount);
            return new Relay()
            {
                Id = -1,
                Positions = members.Select((x, i) => new RelayPosition()
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

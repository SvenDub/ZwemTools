using Schotejil.Clubkampioen.Data.Sql;

namespace Schotejil.Clubkampioen;
public class BoomsmaService
{
    public BoomsmaResults CalculateResults(Session from, Session to)
    {
        IDictionary<(Stroke, Gender), ICollection<BoomsmaResult>> boomsmaResults = new Dictionary<(Stroke, Gender), ICollection<BoomsmaResult>>();

        Stroke[] strokes = new[]
        {
            Stroke.Fly,
            Stroke.Backstroke,
            Stroke.Breaststroke,
            Stroke.Freestyle,
        };

        Gender[] genders = new[]
        {
            Gender.Male,
            Gender.Female,
        };

        foreach (Stroke stroke in strokes)
        {
            foreach (Gender gender in genders)
            {
                ICollection<BoomsmaResult> resultsForStrokeGender = new Collection<BoomsmaResult>();
                foreach (Event @event in to.Events)
                {
                    foreach (Result result in @event.Results)
                    {
                        Athlete athlete = result.Athlete;
                        List<Result> eventResults = from.Events
                            .SelectMany(x => x.Results)
                            .Where(MatchResult(stroke, athlete))
                            .ToList();
                        Result? fromResult = eventResults.SingleOrDefault();
                        if (@event.SwimStyle.Stroke == stroke && athlete.Gender == gender)
                        {

                            resultsForStrokeGender.Add(new BoomsmaResult(athlete, fromResult, result, fromResult is { Status: null} && result is { Status: null } ? this.CalculateDifference(fromResult, result) : null));
                        }
                    }
                }

                boomsmaResults[(stroke, gender)] = resultsForStrokeGender
                    .OrderBy(x => x.Difference ?? TimeSpan.MaxValue)
                    .ToList();
            }
        }

        return new BoomsmaResults(boomsmaResults);
    }

    private static Func<Result, bool> MatchResult(Stroke stroke, Athlete athlete)
    {
        return x =>
        {
            return x.Event.SwimStyle.Stroke == stroke
                && (
                    (
                        x.Athlete.License != null
                        && x.Athlete.License == athlete.License
                    )
                    ||
                    (
                        x.Athlete.FirstName == athlete.FirstName
                        && x.Athlete.LastName == athlete.LastName
                        && x.Athlete.NamePrefix == athlete.NamePrefix
                        && x.Athlete.Birthdate == athlete.Birthdate
                    )
                );
        };
    }

    private TimeSpan CalculateDifference(Result from, Result to)
    {
        double factor = (double)to.Event.SwimStyle.Distance / from.Event.SwimStyle.Distance;
        TimeSpan convertedTime = from.SwimTime.Multiply(factor).Add(TimeSpan.FromSeconds(5 * factor - 5));
        return to.SwimTime - convertedTime;
    }
}

// <copyright file="BoomsmaService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using ZwemTools.Data.Sql;

namespace ZwemTools;

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

                            resultsForStrokeGender.Add(new BoomsmaResult(athlete, fromResult, result, fromResult is { Status: not ResultStatus.Withdrawn } && result is { Status: not ResultStatus.Withdrawn } ? this.CalculateDifference(fromResult, result) : null));
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

                        x.Athlete.License != null
                        && x.Athlete.License == athlete.License

                    ||

                        x.Athlete.FirstName == athlete.FirstName
                        && x.Athlete.LastName == athlete.LastName
                        && x.Athlete.NamePrefix == athlete.NamePrefix
                        && x.Athlete.Birthdate == athlete.Birthdate

                );
        };
    }

    private TimeSpan CalculateDifference(Result from, Result to)
    {
        double factor = (double)to.Event.SwimStyle.Distance / from.Event.SwimStyle.Distance;

        TimeSpan fromTime = from.SwimTime;
        TimeSpan toTime = this.CalculateSwimTime(to);

        TimeSpan convertedTime = fromTime.Multiply(factor).Add(TimeSpan.FromSeconds(5 * factor - 5));
        return toTime - convertedTime;
    }

    private TimeSpan CalculateSwimTime(Result result)
    {
        if (result.Status is ResultStatus.Disqualified)
        {
            switch (result.Event.SwimStyle.Distance)
            {
                case 25:
                    return result.SwimTime;
                case 50:
                    return result.SwimTime + TimeSpan.FromSeconds(3);
                case 100:
                    return result.SwimTime + TimeSpan.FromSeconds(6);
                default:
                    throw new ArgumentOutOfRangeException(nameof(result.Event.SwimStyle.Distance), result.Event.SwimStyle.Distance, "No time penalty defined for distance.");
            }
        }

        return result.SwimTime;
    }
}

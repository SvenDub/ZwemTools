namespace ZwemTools.Data.Sql.SplashQuest;

[Owned]
[Index(nameof(Stroke), nameof(AthleteId), IsUnique = true)]
public record Assignment(Guid Id)
{
    required public Stroke Stroke { get; init; }

    required public virtual Athlete Athlete { get; init; }

    required public int AthleteId { get; init; }
}

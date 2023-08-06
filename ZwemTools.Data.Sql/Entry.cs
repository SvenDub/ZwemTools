namespace ZwemTools.Data.Sql;

[Owned]
public record Entry(Guid Id)
{
    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public TimeSpan? EntryTime { get; set; }

    public int AthleteId { get; set; }

    public virtual Athlete Athlete { get; set; } = null!;
}

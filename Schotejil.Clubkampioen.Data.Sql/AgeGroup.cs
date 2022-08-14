namespace Schotejil.Clubkampioen.Data.Sql;

public class AgeGroup
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public Event Event { get; set; } = null!;

    public int MaxAge { get; set; }

    public int MinAge { get; set; }

    public Gender? Gender { get; set; }

    public Collection<Ranking> Rankings { get; set; } = new();
}
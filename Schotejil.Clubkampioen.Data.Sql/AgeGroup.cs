namespace Schotejil.Clubkampioen.Data.Sql;

public class AgeGroup
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public int MaxAge { get; set; }

    public int MinAge { get; set; }

    public Gender? Gender { get; set; }

    public virtual ICollection<Ranking> Rankings { get; set; } = new Collection<Ranking>();
}
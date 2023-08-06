namespace ZwemTools.Data.Sql.SplashQuest;

public record Team(Guid Id)
{
    public virtual ICollection<Athlete> Athletes { get; set; } = new Collection<Athlete>();

    public virtual ICollection<Assignment> Assignments { get; set; } = new Collection<Assignment>();
}

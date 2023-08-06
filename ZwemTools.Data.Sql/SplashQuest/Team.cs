namespace ZwemTools.Data.Sql.SplashQuest;

public record Team(Guid Id)
{
    required public string Name { get; set; }

    public virtual ICollection<Athlete> Athletes { get; set; } = new Collection<Athlete>();

    public virtual IList<Assignment> Assignments { get; set; } = new List<Assignment>();
}

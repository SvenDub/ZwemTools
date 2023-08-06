namespace ZwemTools.Data.Sql.SplashQuest;

public record SplashQuestMeet(Guid Id)
{
    required public virtual Meet Meet { get; init; }

    public virtual ICollection<Team> Teams { get; set; } = new Collection<Team>();
}

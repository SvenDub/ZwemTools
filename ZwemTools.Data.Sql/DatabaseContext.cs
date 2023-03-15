using Microsoft.Extensions.Logging;

namespace ZwemTools.Data.Sql;

public class DatabaseContext : DbContext
{
    public string DbPath { get; }

    public DbSet<AgeGroup> AgeGroups { get; set; } = null!;
    public DbSet<Athlete> Athletes { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Meet> Meets { get; set; } = null!;
    public DbSet<Ranking> Rankings { get; set; } = null!;
    public DbSet<Result> Results { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;

    public DatabaseContext()
    {
        Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        path = Path.Join(path, "ZwemTools");
        Directory.CreateDirectory(path);
        this.DbPath = Path.Join(path, "meets.db");
    }

    public DatabaseContext(ILogger<DatabaseContext> logger)
        : this()
    {
        logger.LogDebug("Using database {DbPath}", this.DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options
        .UseLazyLoadingProxies()
        .UseSqlite($"Data Source={this.DbPath}");
}

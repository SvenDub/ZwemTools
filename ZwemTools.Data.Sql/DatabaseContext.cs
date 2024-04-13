// <copyright file="DatabaseContext.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using ZwemTools.Data.Sql.SplashQuest;

namespace ZwemTools.Data.Sql;

public class DatabaseContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    public DatabaseContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.ApplicationData;
        string path = Environment.GetFolderPath(folder);
        path = Path.Join(path, "ZwemTools");
        Directory.CreateDirectory(path);
        this.DbPath = Path.Join(path, "meets.db");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    public DatabaseContext(ILogger<DatabaseContext> logger)
        : this()
    {
        logger.LogDebug("Using database {DbPath}", this.DbPath);
    }

    public string DbPath { get; }

    required public DbSet<AgeGroup> AgeGroups { get; set; }

    required public DbSet<Athlete> Athletes { get; set; }

    required public DbSet<Club> Clubs { get; set; }

    required public DbSet<Event> Events { get; set; }

    required public DbSet<Meet> Meets { get; set; }

    required public DbSet<Ranking> Rankings { get; set; }

    required public DbSet<Result> Results { get; set; }

    required public DbSet<Entry> Entries { get; set; }

    required public DbSet<Session> Sessions { get; set; }

    /// <summary>
    /// Gets or sets the SplashQuest meets.
    /// </summary>
    required public DbSet<SplashQuestMeet> SplashQuestMeets { get; set; }

    /// <summary>
    /// Gets or sets the SplashQuest teams.
    /// </summary>
    required public DbSet<Team> SplashQuestTeams { get; set; }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlite($"Data Source={this.DbPath}");
}

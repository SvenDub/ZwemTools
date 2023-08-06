// <copyright file="SplashQuestMeet.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql.SplashQuest;

public record SplashQuestMeet(Guid Id)
{
    required public virtual Meet Meet { get; init; }

    public virtual IList<Team> Teams { get; set; } = new List<Team>();
}

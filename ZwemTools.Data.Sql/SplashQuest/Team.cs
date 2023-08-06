// <copyright file="Team.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.Sql.SplashQuest;

public record Team(Guid Id)
{
    required public string Name { get; set; }

    public virtual ICollection<Athlete> Athletes { get; set; } = new Collection<Athlete>();

    public virtual IList<Assignment> Assignments { get; set; } = new List<Assignment>();

    [NotMapped]
    public int Points => this.Athletes.SelectMany(athlete => athlete.Results).Select(result => result.Points).Sum() ?? 0;
}

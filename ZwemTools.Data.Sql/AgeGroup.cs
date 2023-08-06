// <copyright file="AgeGroup.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

public class AgeGroup
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public int MaxAge { get; set; }

    public int MinAge { get; set; }

    public Gender? Gender { get; set; }

    public virtual ICollection<Ranking> Rankings { get; set; } = new Collection<Ranking>();

    required public int LenexId { get; set; }
}

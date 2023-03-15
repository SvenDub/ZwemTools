// <copyright file="Club.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

public class Club
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int MeetId { get; set; }

    public virtual Meet Meet { get; set; } = null!;

    public virtual ICollection<Athlete> Athletes { get; set; } = new Collection<Athlete>();
}
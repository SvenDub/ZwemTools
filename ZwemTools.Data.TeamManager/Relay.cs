﻿// <copyright file="Relay.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Relay
{
    [Column("RELAYSID")]
    required public int Id { get; set; }

    public TimeSpan EntryTime => this.Positions.Aggregate(TimeSpan.Zero, (x, y) => x + y.EntryTimeSpan);

    public ICollection<RelayPosition> Positions { get; set; } = new List<RelayPosition>();

    public bool IsComplete => this.Positions.All(position => position.Member is not null);
}

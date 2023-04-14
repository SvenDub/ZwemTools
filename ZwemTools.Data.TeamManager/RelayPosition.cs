// <copyright file="RelayPosition.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record RelayPosition
{
    [Column("RELAYSID")]
    required public int RelayId { get; init; }

    [Column("NUMB")]
    required public int Number { get; set; }

    [Column("ENTRYTIME")]
    required public int EntryTime { get; set; }

    public TimeSpan EntryTimeSpan => TimeSpan.FromMilliseconds(this.EntryTime);

    public Member? Member { get; set; }

    public Stroke? Stroke { get; set; }
}

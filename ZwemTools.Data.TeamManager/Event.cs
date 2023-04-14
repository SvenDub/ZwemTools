// <copyright file="Event.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Event
{
    [Column("EVENTSID")]
    required public int Id { get; init; }

    [Column("MEETSID")]
    required public int MeetId { get; set; }

    [Column("NUMB")]
    required public int Number { get; set; }

    [Column("MINAGE")]
    required public int MinAge { get; set; }

    [Column("MAXAGE")]
    required public int MaxAge { get; set; }

    [Column("GENDER")]
    required public Gender Gender { get; set; }

    public SwimStyle? SwimStyle { get; set; }
}

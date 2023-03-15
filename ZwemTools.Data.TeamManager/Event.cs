// <copyright file="Event.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Event
{
    [Column("EVENTSID")]
    public required int Id { get; init; }

    [Column("MEETSID")]
    public required int MeetId { get; set; }

    [Column("NUMB")]
    public required int Number { get; set; }

    [Column("MINAGE")]
    public required int MinAge { get; set; }

    [Column("MAXAGE")]
    public required int MaxAge { get; set; }

    [Column("GENDER")]
    public required Gender Gender { get; set; }

    public SwimStyle? SwimStyle { get; set; }
}

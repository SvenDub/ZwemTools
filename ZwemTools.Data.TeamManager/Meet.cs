// <copyright file="Meet.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Meet
{
    [Column("MEETSID")]
    public required int Id { get; init; }

    [Column("NAME")]
    public required string Name { get; set; }

    [Column("MAXDATE")]
    public required DateTime MaxDate { get; set; }

    [Column("PLACE")]
    public required string Place { get; set; }

    [Column("AGEDATE")]
    public required DateTime AgeDate { get; set; }

    /// <summary>
    /// Gets or sets the minimum date from which entry times can be used.
    /// </summary>
    [Column("ETIMEDATE")]
    public required DateTime EntryTimeDate { get; set; }
}

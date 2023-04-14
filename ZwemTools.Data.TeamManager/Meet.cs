// <copyright file="Meet.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Meet
{
    [Column("MEETSID")]
    required public int Id { get; init; }

    [Column("NAME")]
    required public string Name { get; set; }

    [Column("MAXDATE")]
    required public DateTime MaxDate { get; set; }

    [Column("PLACE")]
    required public string Place { get; set; }

    [Column("AGEDATE")]
    required public DateTime AgeDate { get; set; }

    [Column("AGECALCTYPE")]
    required public AgeCalcType AgeCalcType { get; set; }

    /// <summary>
    /// Gets or sets the minimum date from which entry times can be used.
    /// </summary>
    [Column("ETIMEDATE")]
    required public DateTime EntryTimeDate { get; set; }
}

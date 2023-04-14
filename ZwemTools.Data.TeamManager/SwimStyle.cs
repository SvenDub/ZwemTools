// <copyright file="SwimStyle.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public class SwimStyle
{
    [Column("SWIMSTYLEID")]
    required public int Id { get; set; }

    [Column("DISTANCE")]
    required public int Distance { get; set; }

    [Column("RELAYCOUNT")]
    required public int RelayCount { get; set; }

    [Column("STROKE")]
    required public Stroke Stroke { get; set; }
}

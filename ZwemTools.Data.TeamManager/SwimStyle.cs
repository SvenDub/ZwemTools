// <copyright file="SwimStyle.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public class SwimStyle
{
    [Column("SWIMSTYLEID")]
    public required int Id { get; set; }

    [Column("DISTANCE")]
    public required int Distance { get; set; }

    [Column("RELAYCOUNT")]
    public required int RelayCount { get; set; }

    [Column("STROKE")]
    public required Stroke Stroke { get; set; }
}

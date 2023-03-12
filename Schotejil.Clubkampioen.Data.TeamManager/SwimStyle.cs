﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;

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
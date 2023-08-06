// <copyright file="Result.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.Sql;

public class Result
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public int? Lane { get; set; }

    public TimeSpan SwimTime { get; set; }

    public int AthleteId { get; set; }

    public virtual Athlete Athlete { get; set; } = null!;

    public ResultStatus? Status { get; set; }

    required public int LenexId { get; set; }

    required public int LenexEventId { get; set; }

    public int? Points { get; set; }

    [NotMapped]
    public TimeSpan SwimTimeWithPenalty
    {
        get
        {
            if (this.Status is ResultStatus.Disqualified)
            {
                return this.Event.SwimStyle.Distance switch
                {
                    25 => this.SwimTime,
                    50 => this.SwimTime + TimeSpan.FromSeconds(3),
                    100 => this.SwimTime + TimeSpan.FromSeconds(6),
                    _ => throw new ArgumentOutOfRangeException(nameof(this.Event.SwimStyle.Distance), this.Event.SwimStyle.Distance, "No time penalty defined for distance."),
                };
            }

            return this.SwimTime;
        }
    }
}

﻿namespace Schotejil.Clubkampioen.Data.Sql;

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
}
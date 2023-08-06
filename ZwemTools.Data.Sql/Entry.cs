// <copyright file="Entry.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

[Owned]
public record Entry(Guid Id)
{
    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public TimeSpan? EntryTime { get; set; }

    public int AthleteId { get; set; }

    public virtual Athlete Athlete { get; set; } = null!;

    public int? Lane { get; set; }

    public Guid? HeatId { get; set; }

    public virtual Heat? Heat { get; set; }

    public int? LenexHeatId { get; set; }

    required public int LenexEventId { get; set; }

    public Course? Course { get; set; }
}

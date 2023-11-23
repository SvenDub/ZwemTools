// <copyright file="Event.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

public record Event
{
    public int Id { get; set; }

    public virtual ICollection<AgeGroup> AgeGroups { get; set; } = new Collection<AgeGroup>();

    public TimeOnly? Time { get; set; }

    public Gender? Gender { get; set; }

    public int Number { get; set; }

    public int SessionId { get; set; }

    public virtual Session Session { get; set; } = null!;

    public virtual SwimStyle SwimStyle { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new Collection<Result>();

    public virtual ICollection<Entry> Entries { get; set; } = new Collection<Entry>();

    public virtual ICollection<Heat> Heats { get; set; } = new Collection<Heat>();

    required public int LenexId { get; set; }
}

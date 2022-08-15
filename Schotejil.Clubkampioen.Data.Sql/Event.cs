﻿namespace Schotejil.Clubkampioen.Data.Sql;

public class Event
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
}
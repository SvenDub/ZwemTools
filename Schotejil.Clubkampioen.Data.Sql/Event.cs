using System.Reflection;

namespace Schotejil.Clubkampioen.Data.Sql;

public class Event
{
    public int Id { get; set; }

    public Collection<AgeGroup> AgeGroups { get; set; } = new();

    public TimeOnly? Time { get; set; }

    public Gender? Gender { get; set; }

    public int Number { get; set; }

    public int SessionId { get; set; }

    public Session Session { get; set; } = null!;

    public SwimStyle SwimStyle { get; set; } = null!;

    public Collection<Result> Results { get; set; } = new();
}
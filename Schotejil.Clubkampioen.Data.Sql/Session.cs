namespace Schotejil.Clubkampioen.Data.Sql;

public class Session
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new Collection<Event>();

    public string? Name { get; set; }

    public int Number { get; set; }

    public int MeetId { get; set; }

    public virtual Meet Meet { get; set; } = null!;

    public TimeOnly? OfficialMeeting { get; set; }

    public TimeOnly? WarmupStart { get; set; }

    public TimeOnly? WarmupEnd { get; set; }
}
namespace Schotejil.Clubkampioen.Data.Sql;

public class Result
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public Event Event { get; set; } = null!;

    public int? Lane { get; set; }

    public TimeSpan SwimTime { get; set; }
}
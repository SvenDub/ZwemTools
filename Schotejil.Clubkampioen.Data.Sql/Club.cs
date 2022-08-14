namespace Schotejil.Clubkampioen.Data.Sql;

public class Club
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int MeetId { get; set; }

    public Meet Meet { get; set; } = null!;

    public Collection<Athlete> Athletes { get; set; } = new();
}
namespace Schotejil.Clubkampioen.Data.Sql;

public class Meet
{
    public int Id { get; set; }

    public virtual AgeDate? AgeDate { get; set; }

    public string City { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public Course? Course { get; set; }

    public string Nation { get; set; } = string.Empty;

    public string? Organizer { get; set; }

    public string? OrganizerUrl { get; set; }

    public string? LiveTiming { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new Collection<Session>();

    public virtual ICollection<Club> Clubs { get; set; } = new Collection<Club>();
}

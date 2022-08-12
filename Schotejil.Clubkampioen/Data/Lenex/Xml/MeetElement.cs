using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class MeetElement
{
    [XmlElement("AGEDATE")]
    public AgeDateElement? AgeDate { get; set; } = new();

    [XmlAttribute("city")]
    public string City { get; set; } = string.Empty;

    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("course")]
    public string? CourseString {
        get { return this.Course?.ToXmlString(); }
        set { this.Course = value is not null ? XmlEnumExtensions.FromXmlString<Course>(value) : null; }
    }

    [XmlIgnore]
    public Course? Course { get; set; }

    [XmlAttribute("nation")]
    public string Nation { get; set; } = string.Empty;

    [XmlAttribute("organizer")]
    public string? Organizer { get; set; }

    [XmlAttribute("organizer.url")]
    public string? OrganizerUrl { get; set; }

    [XmlAttribute("result.url")]
    public string? LiveTiming { get; set; }
}

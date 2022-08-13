using System.Globalization;
using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class EventElement
{
    [XmlArray("AGEGROUPS")]
    [XmlArrayItem("AGEGROUP")]
    public Collection<AgeGroupElement> AgeGroups { get; set; } = new();

    [XmlAttribute("daytime")]
    public string? TimeProxy {
        get => Time?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => Time = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? Time { get; set; }

    [XmlAttribute("eventid")]
    public int EventId { get; set; }

    [XmlAttribute("gender")]
    public string? GenderProxy {
        get => Gender?.ToXmlString();
        set => Gender = value is not null ? XmlEnumExtensions.FromXmlString<Gender>(value) : null;
    }

    [XmlIgnore]
    public Gender? Gender { get; set; }

    [XmlAttribute("number")]
    public int Number { get; set; }

    [XmlElement("SWIMSTYLE")]
    public SwimStyleElement SwimStyle { get; set; } = new();
}
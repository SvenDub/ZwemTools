using System.Globalization;

namespace ZwemTools.Data.Lenex.Xml;

public class EventElement
{
    [XmlArray("AGEGROUPS")]
    [XmlArrayItem("AGEGROUP")]
    public Collection<AgeGroupElement> AgeGroups { get; set; } = new();

    [XmlAttribute("daytime")]
    public string? TimeProxy
    {
        get => this.Time?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.Time = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? Time { get; set; }

    [XmlAttribute("eventid")]
    public int EventId { get; set; }

    [XmlAttribute("gender")]
    public string? GenderProxy
    {
        get => this.Gender?.ToXmlString();
        set => this.Gender = XmlEnumExtensions.FromXmlString<Gender>(value);
    }

    [XmlIgnore]
    public Gender? Gender { get; set; }

    [XmlAttribute("number")]
    public int Number { get; set; }

    [XmlElement("SWIMSTYLE")]
    public SwimStyleElement SwimStyle { get; set; } = new();

    [XmlArray("HEATS")]
    [XmlArrayItem("HEAT")]
    public Collection<HeatElement> Heats { get; set; } = new();
}
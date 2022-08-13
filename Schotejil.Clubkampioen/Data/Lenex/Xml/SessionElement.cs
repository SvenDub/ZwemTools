using System.Globalization;
using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;
public class SessionElement
{
    [XmlAttribute("date", DataType = "date")]
    public DateTime Date { get; set; }

    [XmlAttribute("daytime")]
    public string? StartTimeProxy
    {
        get => StartTime?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => StartTime = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? StartTime { get; set; }

    [XmlAttribute("endtime")]
    public string? EndTimeProxy
    {
        get => EndTime?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => EndTime = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? EndTime { get; set; }

    [XmlArray("EVENTS")]
    [XmlArrayItem("EVENT")]
    public Collection<EventElement> Events { get; set; } = new();

    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("number")]
    public int Number { get; set; }

    [XmlAttribute("officialmeeting")]
    public string? OfficialMeetingProxy
    {
        get => OfficialMeeting?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => OfficialMeeting = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? OfficialMeeting { get; set; }

    [XmlAttribute("warmupfrom")]
    public string? WarmupStartProxy
    {
        get => WarmupStart?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => WarmupStart = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? WarmupStart { get; set; }

    [XmlAttribute("warmupuntil")]
    public string? WarmupEndProxy
    {
        get => WarmupEnd?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => WarmupEnd = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? WarmupEnd { get; set; }
}

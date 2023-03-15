using System.Globalization;

namespace ZwemTools.Data.Lenex.Xml;
public class SessionElement
{
    [XmlAttribute("date", DataType = "date")]
    public DateTime Date { get; set; }

    [XmlAttribute("daytime")]
    public string? StartTimeProxy
    {
        get => this.StartTime?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.StartTime = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? StartTime { get; set; }

    [XmlAttribute("endtime")]
    public string? EndTimeProxy
    {
        get => this.EndTime?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.EndTime = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
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
        get => this.OfficialMeeting?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.OfficialMeeting = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? OfficialMeeting { get; set; }

    [XmlAttribute("warmupfrom")]
    public string? WarmupStartProxy
    {
        get => this.WarmupStart?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.WarmupStart = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? WarmupStart { get; set; }

    [XmlAttribute("warmupuntil")]
    public string? WarmupEndProxy
    {
        get => this.WarmupEnd?.ToString("HH:mm", CultureInfo.InvariantCulture);
        set => this.WarmupEnd = value is not null ? TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture) : null;
    }

    [XmlIgnore]
    public TimeOnly? WarmupEnd { get; set; }
}

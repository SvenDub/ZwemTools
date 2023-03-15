using System.Globalization;

namespace ZwemTools.Data.Lenex.Xml;

public class EntryElement
{
    [XmlAttribute("entrycourse")]
    public string? CourseString
    {
        get => this.Course?.ToXmlString();
        set => this.Course = XmlEnumExtensions.FromXmlString<Course>(value);
    }

    [XmlIgnore]
    public Course? Course { get; set; }

    [XmlAttribute("entrytime")]
    public string EntryTimeProxy
    {
        get => this.EntryTime is null ? "NT" : this.EntryTime.Value.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
        set => this.EntryTime = value == "NT" ? null : TimeSpan.Parse(value);
    }

    [XmlIgnore]
    public TimeSpan? EntryTime { get; set; }

    [XmlAttribute("eventid")]
    public int EventId { get; set; }

    [XmlAttribute("heatid")]
    public string? HeatIdProxy
    {
        get => this.HeatId?.ToString();
        set => this.HeatId = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? HeatId { get; set; }

    [XmlAttribute("lane")]
    public string? LaneProxy
    {
        get => this.Lane?.ToString();
        set => this.Lane = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? Lane { get; set; }
}
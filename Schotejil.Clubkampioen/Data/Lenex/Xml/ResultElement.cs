using System.Globalization;
using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class ResultElement
{
    [XmlAttribute("eventid")]
    public int EventId { get; set; }

    [XmlAttribute("heatid")]
    public string? HeatIdProxy
    {
        get => HeatId?.ToString();
        set => HeatId = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? HeatId { get; set; }

    [XmlAttribute("lane")]
    public string? LaneProxy
    {
        get => Lane?.ToString();
        set => Lane = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? Lane { get; set; }

    [XmlAttribute("resultid")]
    public int ResultId { get; set; }

    [XmlAttribute("swimtime")]
    public string SwimTimeProxy
    {
        get => SwimTime.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
        set => SwimTime = TimeSpan.Parse(value);
    }

    [XmlIgnore]
    public TimeSpan SwimTime { get; set; }
}
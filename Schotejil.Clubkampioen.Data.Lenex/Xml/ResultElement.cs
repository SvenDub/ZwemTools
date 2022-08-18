using System.Globalization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class ResultElement
{
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

    [XmlAttribute("resultid")]
    public int ResultId { get; set; }

    [XmlAttribute("swimtime")]
    public string SwimTimeProxy
    {
        get => this.SwimTime.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
        set => this.SwimTime = TimeSpan.Parse(value);
    }

    [XmlIgnore]
    public TimeSpan SwimTime { get; set; }

    [XmlAttribute("status")]
    public string? StatusProxy
    {
        get => this.Status?.ToXmlString();
        set => this.Status = XmlEnumExtensions.FromXmlString<ResultStatus>(value);
    }

    [XmlIgnore]
    public ResultStatus? Status { get; set; }
}
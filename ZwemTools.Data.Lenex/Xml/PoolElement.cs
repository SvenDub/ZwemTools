namespace ZwemTools.Data.Lenex.Xml;

public record PoolElement
{
    [XmlIgnore]
    public int? LaneMin { get; set; }

    [XmlAttribute("lanemin")]
    public string? LaneMinProxy
    {
        get => this.LaneMin?.ToString();
        set => this.LaneMin = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? LaneMax { get; set; }

    [XmlAttribute("lanemax")]
    public string? LaneMaxProxy
    {
        get => this.LaneMax?.ToString();
        set => this.LaneMax = value is not null ? int.Parse(value) : null;
    }
}

namespace ZwemTools.Data.Lenex.Xml;

public class RankingElement
{
    [XmlAttribute("order")]
    public string? OrderProxy
    {
        get => this.Order?.ToString();
        set => this.Order = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? Order { get; set; }

    [XmlAttribute("place")]
    public int Place { get; set; }

    [XmlAttribute("resultid")]
    public int ResultId { get; set; }
}
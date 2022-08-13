using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class RankingElement
{
    [XmlAttribute("order")]
    public string? OrderProxy
    {
        get => Order?.ToString();
        set => Order = value is not null ? int.Parse(value) : null;
    }

    [XmlIgnore]
    public int? Order { get; set; }

    [XmlAttribute("place")]
    public int Place { get; set; }

    [XmlAttribute("resultid")]
    public int ResultId { get; set; }
}
namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class AgeGroupElement
{
    [XmlAttribute("agegroupid")]
    public int AgeGroupId { get; set; }

    [XmlAttribute("agemax")]
    public int MaxAge { get; set; }

    [XmlAttribute("agemin")]
    public int MinAge { get; set; }

    [XmlIgnore]
    public Gender? Gender { get; set; }

    [XmlAttribute("gender")]
    public string? GenderProxy {
        get => this.Gender?.ToXmlString();
        set => this.Gender = XmlEnumExtensions.FromXmlString<Gender>(value);
    }

    [XmlArray("RANKINGS")]
    [XmlArrayItem("RANKING")]
    public Collection<RankingElement> Rankings { get; set; } = new();
}
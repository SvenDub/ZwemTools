namespace Schotejil.Clubkampioen.Data.Lenex.Xml;
public class AgeDateElement
{
    [XmlIgnore]
    public AgeDateType Type { get; set; }

    [XmlAttribute("type")]
    public string TypeProxy
    {
        get => this.Type.ToXmlString();
        set => this.Type = XmlEnumExtensions.FromXmlString<AgeDateType>(value);
    }

    [XmlAttribute("value", DataType = "date")]
    public DateTime Value { get; set; }
}

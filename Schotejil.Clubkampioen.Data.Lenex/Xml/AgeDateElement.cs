namespace Schotejil.Clubkampioen.Data.Lenex.Xml;
public class AgeDateElement
{
    [XmlIgnore]
    public AgeDateType Type { get; set; }

    [XmlAttribute("type")]
    public string TypeProxy
    {
        get => Type.ToXmlString();
        set => Type = XmlEnumExtensions.FromXmlString<AgeDateType>(value);
    }

    [XmlAttribute("value", DataType = "date")]
    public DateTime Value { get; set; }
}

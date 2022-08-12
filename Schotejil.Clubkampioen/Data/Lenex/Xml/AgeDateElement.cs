using System.Reflection;
using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;
public class AgeDateElement
{
    [XmlIgnore]
    public AgeDateType Type { get; set; }

    [XmlAttribute("type")]
    public string TypeString
    {
        get
        {
            return Type.ToXmlString();
        }
        set
        {
            Type = XmlEnumExtensions.FromXmlString<AgeDateType>(value);
        }
    }

    [XmlAttribute("value")]
    public DateTime Value { get; set; }
}

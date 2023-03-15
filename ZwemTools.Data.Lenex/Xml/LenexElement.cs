namespace ZwemTools.Data.Lenex.Xml;

[XmlRoot("LENEX")]
public class LenexElement
{
    [XmlAttribute("version")]
    public string Version { get; set; } = string.Empty;

    [XmlElement("CONSTRUCTOR")]
    public ConstructorElement Constructor { get; set; } = new();


    [XmlArray("MEETS")]
    [XmlArrayItem("MEET")]
    public Collection<MeetElement> Meets { get; set; } = new();

    /*
[XmlArray("RECORDLISTS")]
[XmlArrayItem("RECORDLIST")]
List<RecordListElement>? RecordLists,

[XmlArray("TIMESTANDARDLISTS")]
[XmlArrayItem("TIMESTANDARDLIST")]
List<TimeStandardListElement>? TimeStandardLists*/
}

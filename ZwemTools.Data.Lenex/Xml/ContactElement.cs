namespace ZwemTools.Data.Lenex.Xml;

public class ContactElement
{
    [XmlAttribute("email")]
    public string Email { get; set; } = string.Empty;
}

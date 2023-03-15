namespace ZwemTools.Data.Lenex.Xml;

public class AthleteElement
{
    [XmlAttribute("athleteid")]
    public int AthleteId { get; set; }

    [XmlAttribute("birthdate", DataType = "date")]
    public DateTime Birthdate { get; set; }

    [XmlAttribute("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [XmlIgnore]
    public Gender Gender { get; set; }

    [XmlAttribute("gender")]
    public string GenderProxy
    {
        get => this.Gender.ToXmlString();
        set => this.Gender = XmlEnumExtensions.FromXmlString<Gender>(value);
    }

    [XmlAttribute("lastname")]
    public string LastName { get; set; } = string.Empty;

    [XmlAttribute("license")]
    public string? License { get; set; }

    [XmlAttribute("nameprefix")]
    public string? NamePrefix { get; set; }

    [XmlArray("RESULTS")]
    [XmlArrayItem("RESULT")]
    public Collection<ResultElement> Results { get; set; } = new();

    [XmlArray("ENTRIES")]
    [XmlArrayItem("ENTRY")]
    public Collection<EntryElement> Entries { get; set; } = new();

    [XmlIgnore]
    public string FullName => string.Join(" ", new[] { this.FirstName, this.NamePrefix, this.LastName }.Where(x => x is not null));
}
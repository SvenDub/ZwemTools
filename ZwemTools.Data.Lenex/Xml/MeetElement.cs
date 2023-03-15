// <copyright file="MeetElement.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public class MeetElement
{
    [XmlElement("AGEDATE")]
    public AgeDateElement? AgeDate { get; set; } = new();

    [XmlAttribute("city")]
    public string City { get; set; } = string.Empty;

    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("course")]
    public string? CourseString
    {
        get => this.Course?.ToXmlString();
        set => this.Course = XmlEnumExtensions.FromXmlString<Course>(value);
    }

    [XmlIgnore]
    public Course? Course { get; set; }

    [XmlAttribute("nation")]
    public string Nation { get; set; } = string.Empty;

    [XmlAttribute("organizer")]
    public string? Organizer { get; set; }

    [XmlAttribute("organizer.url")]
    public string? OrganizerUrl { get; set; }

    [XmlAttribute("result.url")]
    public string? LiveTiming { get; set; }

    [XmlArray("SESSIONS")]
    [XmlArrayItem("SESSION")]
    public Collection<SessionElement> Sessions { get; set; } = new();

    [XmlArray("CLUBS")]
    [XmlArrayItem("CLUB")]
    public Collection<ClubElement> Clubs { get; set; } = new();

    public AthleteElement? GetAthleteForResult(int resultId) => this.Clubs
        .SelectMany(c => c.Athletes)
        .FirstOrDefault(a => a.Results.Any(r => r.ResultId == resultId));

    public ResultElement? GetResult(int resultId) => this.Clubs
        .SelectMany(c => c.Athletes)
        .SelectMany(a => a.Results)
        .FirstOrDefault(r => r.ResultId == resultId);
}

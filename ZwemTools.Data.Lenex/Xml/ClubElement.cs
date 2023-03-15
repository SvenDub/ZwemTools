// <copyright file="ClubElement.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public class ClubElement
{
    [XmlAttribute("clubid")]
    public int ClubId { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlArray("ATHLETES")]
    [XmlArrayItem("ATHLETE")]
    public Collection<AthleteElement> Athletes { get; set; } = new();
}

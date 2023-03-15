// <copyright file="ConstructorElement.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public class ConstructorElement
{
    [XmlElement("CONTACT")]
    public ContactElement Contact { get; set; } = new();

    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("registration")]
    public string? Registration { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; } = string.Empty;
}

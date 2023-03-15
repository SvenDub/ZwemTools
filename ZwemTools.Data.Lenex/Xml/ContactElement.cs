// <copyright file="ContactElement.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public class ContactElement
{
    [XmlAttribute("email")]
    public string Email { get; set; } = string.Empty;
}

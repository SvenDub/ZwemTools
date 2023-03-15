// <copyright file="HeatElement.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public class HeatElement
{
    [XmlAttribute("heatid")]
    public int HeatId { get; set; }

    [XmlAttribute("number")]
    public int Number { get; set; }
}

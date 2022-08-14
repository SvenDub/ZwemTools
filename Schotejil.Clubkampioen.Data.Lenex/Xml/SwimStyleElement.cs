﻿namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public class SwimStyleElement
{
    [XmlAttribute("distance")]
    public int Distance { get; set; }

    [XmlAttribute("relaycount")]
    public int RelayCount { get; set; }

    [XmlAttribute("stroke")]
    public string StrokeProxy
    {
        get => this.Stroke.ToXmlString();
        set => this.Stroke = XmlEnumExtensions.FromXmlString<Stroke>(value);
    }

    [XmlIgnore]
    public Stroke Stroke { get; set; }
}
﻿using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public enum Gender
{
    [XmlEnum("A")]
    All,
    [XmlEnum("F")]
    Female,
    [XmlEnum("M")]
    Male,
    [XmlEnum("X")]
    Mixed,
}
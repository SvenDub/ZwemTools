// <copyright file="AgeDateType.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

public enum AgeDateType
{
    [XmlEnum("YEAR")]
    Year,
    [XmlEnum("DATE")]
    Date,
    [XmlEnum("POR")]
    Por,
    [XmlEnum("CAN.FNQ")]
    CanFnq,
    [XmlEnum("LUX")]
    Lux,
}

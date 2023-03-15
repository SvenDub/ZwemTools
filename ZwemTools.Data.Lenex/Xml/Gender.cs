// <copyright file="Gender.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Lenex.Xml;

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
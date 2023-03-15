// <copyright file="SwimStyle.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

[Owned]
public class SwimStyle
{
    public int Distance { get; set; }

    public int RelayCount { get; set; }

    public Stroke Stroke { get; set; }
}

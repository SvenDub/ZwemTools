// <copyright file="AgeDate.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.Sql;

[Owned]
public record AgeDate
{
    public AgeDateType Type { get; set; }

    public DateOnly Value { get; set; }
}

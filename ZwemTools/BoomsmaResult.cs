// <copyright file="BoomsmaResult.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using ZwemTools.Data.Sql;

namespace ZwemTools;

public record BoomsmaResult(
    Athlete Athlete,
    Result? FromResult,
    Result? ToResult,
    TimeSpan? Difference);

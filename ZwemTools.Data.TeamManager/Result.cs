// <copyright file="Result.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Result
{
    [Column("RESULTSID")]
    required public int Id { get; init; }

    public Member? Member { get; set; }
}

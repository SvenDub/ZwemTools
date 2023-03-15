// <copyright file="Group.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;
public record Group
{
    [Column("GROUPSID")]
    public required string Id { get; set; }

    [Column("NAME")]
    public required string Name { get; set; }
}

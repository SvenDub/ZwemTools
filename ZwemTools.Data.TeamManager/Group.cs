// <copyright file="Group.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Group
{
    [Column("GROUPSID")]
    required public string Id { get; set; }

    [Column("NAME")]
    required public string Name { get; set; }
}

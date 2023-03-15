// <copyright file="Athlete.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.Sql;

public class Athlete
{
    public int Id { get; set; }

    public DateOnly Birthdate { get; set; }

    public int ClubId { get; set; }

    public virtual Club Club { get; set; } = null!;

    public string FirstName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string? License { get; set; }

    public string? NamePrefix { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new Collection<Result>();

    [NotMapped]
    public string FullName => string.Join(" ", new[] { this.FirstName, this.NamePrefix, this.LastName }.Where(x => x is not null));
}

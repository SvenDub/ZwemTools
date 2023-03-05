using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;

public record Member
{
    [Column("MEMBERSID")]
    public required int Id { get; init; }

    [Column("LASTNAME")]
    public required string Lastname { get; set; }

    [Column("FIRSTNAME")]
    public required string Firstname { get; set; }

    [Column("BIRTHDATE")]
    public required DateTime Birthdate { get; set; }
}
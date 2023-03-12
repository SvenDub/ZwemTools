using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;

public record Meet
{
    [Column("MEETSID")]
    public required int Id { get; init; }

    [Column("NAME")]
    public required string Name { get; set; }

    [Column("MAXDATE")]
    public required DateTime MaxDate { get; set; }

    [Column("PLACE")]
    public required string Place { get; set; }

    [Column("AGEDATE")]
    public required DateTime AgeDate { get; set; }

}

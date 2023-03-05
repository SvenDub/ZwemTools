using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;
public record Result
{
    [Column("RESULTSID")]
    public required int Id { get; init; }

    public Member? Member { get; set; }
}

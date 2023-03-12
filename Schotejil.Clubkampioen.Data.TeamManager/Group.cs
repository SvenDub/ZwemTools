using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;
public record Group
{
    [Column("GROUPSID")]
    public required string Id { get; set; }

    [Column("NAME")]
    public required string Name { get; set; }
}

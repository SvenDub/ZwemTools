using System.ComponentModel.DataAnnotations.Schema;

namespace Schotejil.Clubkampioen.Data.TeamManager;
public record Relay
{
    [Column("RELAYSID")]
    public required int Id { get; set; }

    public TimeSpan EntryTime => this.Positions.Aggregate(TimeSpan.Zero, (x, y) => x + y.EntryTimeSpan);

    public ICollection<RelayPosition> Positions { get; set; } = new List<RelayPosition>();
}

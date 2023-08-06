using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.Sql;

[Owned]
public record Pool
{
    public int? LaneMin { get; set; }

    public int? LaneMax { get; set; }

    [NotMapped]
    public int? Lanes
    {
        get
        {
            if (this.LaneMin is { } laneMin && this.LaneMax is { } laneMax)
            {
                return laneMax - laneMin + 1;
            }

            return null;
        }
    }
}

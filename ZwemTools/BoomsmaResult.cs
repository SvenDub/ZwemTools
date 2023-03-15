using ZwemTools.Data.Sql;

namespace ZwemTools;

public record BoomsmaResult(
    Athlete Athlete,
    Result? FromResult,
    Result? ToResult,
    TimeSpan? Difference
);
using Schotejil.Clubkampioen.Data.Sql;

namespace Schotejil.Clubkampioen;

public record BoomsmaResult(
    Athlete Athlete,
    Result? FromResult,
    Result? ToResult,
    TimeSpan? Difference
);
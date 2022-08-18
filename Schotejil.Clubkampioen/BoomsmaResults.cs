using Schotejil.Clubkampioen.Data.Sql;

namespace Schotejil.Clubkampioen;

public record BoomsmaResults(
    IDictionary<(Stroke Stroke, Gender Gender), ICollection<BoomsmaResult>> Results
)
{
    public ICollection<BoomsmaResult> GetResult(Stroke stroke, Gender gender)
    {
        if (this.Results.TryGetValue((stroke, gender), out ICollection<BoomsmaResult>? results)) {
            return results;
        }

        return new Collection<BoomsmaResult>();
    }
};

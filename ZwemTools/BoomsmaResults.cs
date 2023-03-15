// <copyright file="BoomsmaResults.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using ZwemTools.Data.Sql;

namespace ZwemTools;

public record BoomsmaResults(IDictionary<(Stroke Stroke, Gender Gender), ICollection<BoomsmaResult>> Results)
{
    public ICollection<BoomsmaResult> GetResult(Stroke stroke, Gender gender)
    {
        if (this.Results.TryGetValue((stroke, gender), out ICollection<BoomsmaResult>? results))
        {
            return results;
        }

        return new Collection<BoomsmaResult>();
    }
}

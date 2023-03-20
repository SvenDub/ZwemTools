// <copyright file="EnumerableEqualityComparer.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools;

/// <summary>
/// Equality comparer for comparing enumerables. The comparer follows the rules of <see cref="Enumerable.SequenceEqual{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Collections.Generic.IEnumerable{TSource})"/>.
/// </summary>
/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
public class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
{
    /// <inheritdoc/>
    public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.SequenceEqual(y);
    }

    /// <inheritdoc/>
    public int GetHashCode(IEnumerable<T> obj)
    {
        return obj.Aggregate(default(HashCode), (code, arg2) =>
            {
                code.Add(arg2);
                return code;
            })
            .ToHashCode();
    }
}

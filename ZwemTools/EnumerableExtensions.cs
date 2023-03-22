// <copyright file="EnumerableExtensions.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools;

/// <summary>
/// Extensions for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Permutes an enumerable.
    /// </summary>
    /// <param name="sequence">The enumerable.</param>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <returns>An enumerable containing all permutations of the source enumerable.</returns>
    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
    {
        List<T> list = sequence.ToList();

        if (!list.Any())
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            int startingElementIndex = 0;

            foreach (T? startingElement in list)
            {
                int index = startingElementIndex;
                IEnumerable<T> remainingItems = list.Where((_, i) => i != index);

                foreach (IEnumerable<T>? permutationOfRemainder in remainingItems.Permute())
                {
                    yield return permutationOfRemainder.Prepend(startingElement);
                }

                startingElementIndex++;
            }
        }
    }

    /// <summary>
    /// Filters nullable items from an enumerable, leaving only non-null values.
    /// </summary>
    /// <param name="values">The enumerable.</param>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <returns>An enumerable with non-null values.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> values)
        where T : struct
    {
        return values
            .Where(arg => arg.HasValue)
            .Select(arg => arg.Value);
    }

    /// <summary>
    /// Filters nullable items from an enumerable, leaving only non-null values.
    /// </summary>
    /// <param name="values">The enumerable.</param>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <returns>An enumerable with non-null values.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> values)
        where T : notnull
    {
        return values.OfType<T>();
    }
}

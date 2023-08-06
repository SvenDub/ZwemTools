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
            .Select(arg => arg!.Value);
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

    /// <summary>
    /// Returns an enumerable that contains at least the selected amount of elements. If the source enumerable contains fewer elements than the <paramref name="expectedLength"/>,
    /// <c>default</c> elements are added.
    /// </summary>
    /// <param name="values">The enumerable.</param>
    /// <param name="expectedLength">The expected amount of elements.</param>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <returns>An enumerable of at least the expected length.</returns>
    public static IEnumerable<T?> AtLeast<T>(this IEnumerable<T> values, int expectedLength)
    {
        int length = 0;
        foreach (T? value in values)
        {
            length++;
            yield return value;
        }

        for (int i = expectedLength - length - 1; i >= 0; i--)
        {
            yield return default;
        }
    }

    public static Collection<T> ToCollection<T>(this IEnumerable<T> values) => new(values.ToList());
}

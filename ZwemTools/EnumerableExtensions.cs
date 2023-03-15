// <copyright file="EnumerableExtensions.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools;

public static class EnumerableExtensions
{
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

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> values)
        where T : notnull
    {
        return values.OfType<T>();
    }
}

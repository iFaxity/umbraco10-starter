using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Guttew.Umbraco.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    ///     Checks whether given sequence is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements of the sequence.</typeparam>
    /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check.</param>
    /// <returns>Returns <code>true</code> if given sequence is null or is empty</returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source)
    {
        return source?.Any() != true;
    }

    /// <summary>
    /// Applies an action on each item of the sequence.
    /// </summary>
    /// <typeparam name="T">The type of an item.</typeparam>
    /// <param name="source">The source sequence of items.</param>
    /// <param name="action">The action to apply on an item.</param>
    public static void ForEach<T>(this IEnumerable<T>? source, Action<T> action)
    {
        if (IsNullOrEmpty(source))
            return;

        foreach (var item in source)
            action(item);
    }

    /// <summary>
    /// Applies an action on each item of the sequence
    /// </summary>
    /// <typeparam name="T">The type of an item.</typeparam>
    /// <param name="source">The source sequence of items.</param>
    /// <param name="action">The action to apply on an item passed along with the index of the item.</param>
    public static void ForEach<T>(this IEnumerable<T>? source, Action<int, T> action)
    {
        if (IsNullOrEmpty(source))
            return;

        var i = 0;
        foreach (var item in source)
            action(i++, item);
    }

    /// <summary>
    /// Filters the elements of an <see cref="IEnumerable" /> based on a specified type.
    /// Returns empty sequence if source sequence is null.
    /// NOTE: Helper extension to work with legacy APIs which might return null references of IEnumerable.
    /// </summary>
    /// <typeparam name="T">The type to filter the elements of the sequence on.</typeparam>
    /// <param name="source">The <see cref="IEnumerable" /> whose elements to filter.</param>
    /// <returns>
    /// An <see cref="IEnumerable" /> that contains elements from the input sequence of type <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> SafeOfType<T>(this IEnumerable? source)
    {
        if (source == null)
            return Enumerable.Empty<T>();

        return source.OfType<T>();
    }

    /// <summary>
    /// Filters by page and page size.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <typeparam name="T">The type of the source.</typeparam>
    /// <returns>Filtered sequence.</returns>
    public static IEnumerable<T> FilterPaging<T>(this IEnumerable<T> source, int page, int pageSize)
    {
        if (page < 1)
            page = 1;

        if (pageSize < 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        return source
            .Skip(pageSize * (page - 1))
            .Take(pageSize);
    }

    /// <summary>
    /// Transforms item into IEnumerable with one item.
    /// </summary>
    /// <typeparam name="T">The type of elements of the sequence.</typeparam>
    /// <param name="item">Only member of enumerable.</param>
    /// <returns>Enumerable with one item</returns>
    public static IEnumerable<T> Singleton<T>(T item)
    {
        yield return item;
    }

    /// <summary>
    /// Selects distinct values from list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey">Property</typeparam>
    /// <param name="source">The source</param>
    /// <param name="property">Property to distinct by</param>
    /// <returns></returns>
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> property)
    {
        return source
            .GroupBy(property)
            .Select(x => x.First());
    }

    /// <summary>
    /// Sorts the elements of a sequence in the specified order according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <returns></returns>
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, ListSortDirection sortDirection)
    {
        switch (sortDirection)
        {
            case ListSortDirection.Descending:
                return source.OrderByDescending(keySelector);

            case ListSortDirection.Ascending:
            default:
                return source.OrderBy(keySelector);
        }
    }
}

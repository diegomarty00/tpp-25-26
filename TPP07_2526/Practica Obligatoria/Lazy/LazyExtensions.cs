namespace Lazy
;

public static class LazyExtensions
{
    public static IEnumerable<TResult> MapLazy<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }


    public static IEnumerable<T> FilterLazy<T>(
            this IEnumerable<T> source,
            Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                yield return item;
    }

    public static IEnumerable<TResult> ZipLazy<T1, T2, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, T2, TResult> selector)
    {
        using var e1 = first.GetEnumerator();
        using var e2 = second.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext())
            yield return selector(e1.Current, e2.Current);
    }

    public static IEnumerable<T> TakeLazy<T>(this IEnumerable<T> source, int count)
    {
        if (count <= 0)
            yield break;

        int i = 0;
        foreach (var item in source)
        {
            if (i++ >= count)
                yield break;

            yield return item;
        }
    }

    public static IEnumerable<T> TakeWhileLazy<T>(
            this IEnumerable<T> source,
            Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (!predicate(item))
                yield break;

            yield return item;
        }
    }

    public static IEnumerable<T> SkipLazy<T>(this IEnumerable<T> source, int count)
    {
        int skipped = 0;

        foreach (var item in source)
        {
            if (skipped < count)
            {
                skipped++;
                continue;
            }
            yield return item;
        }
    }

    public static IEnumerable<T> SkipWhileLazy<T>(
            this IEnumerable<T> source,
            Func<T, bool> predicate)
    {
        bool skipping = true;

        foreach (var item in source)
        {
            if (skipping && predicate(item))
                continue;

            skipping = false;
            yield return item;
        }
    }
}

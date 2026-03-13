namespace Enumerable;

using System;
using System.Collections;
using System.Collections.Generic;

public class MapEnumerable<TSource, TResult> : IEnumerable<TResult>
{
    private readonly IEnumerable<TSource> source;
    private readonly Func<TSource, TResult> selector;

    public MapEnumerable(IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        this.source = source;
        this.selector = selector;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return new MapEnumerator(source.GetEnumerator(), selector);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class MapEnumerator : IEnumerator<TResult>
    {
        private readonly IEnumerator<TSource> enumerator;
        private readonly Func<TSource, TResult> selector;

        public MapEnumerator(IEnumerator<TSource> enumerator, Func<TSource, TResult> selector)
        {
            this.enumerator = enumerator;
            this.selector = selector;
        }

        public TResult Current => selector(enumerator.Current);
        object IEnumerator.Current => Current;

        public bool MoveNext() => enumerator.MoveNext();
        public void Reset() => enumerator.Reset();
        public void Dispose() => enumerator.Dispose();
    }
}
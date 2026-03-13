namespace Enumerable;

using System;
using System.Collections;
using System.Collections.Generic;

public class ZipEnumerable<T1, T2, TResult> : IEnumerable<TResult>
{
    private readonly IEnumerable<T1> first;
    private readonly IEnumerable<T2> second;
    private readonly Func<T1, T2, TResult> selector;

    public ZipEnumerable(IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, TResult> selector)
    {
        this.first = first;
        this.second = second;
        this.selector = selector;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return new ZipEnumerator(first.GetEnumerator(), second.GetEnumerator(), selector);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class ZipEnumerator : IEnumerator<TResult>
    {
        private readonly IEnumerator<T1> en1;
        private readonly IEnumerator<T2> en2;
        private readonly Func<T1, T2, TResult> selector;

        public ZipEnumerator(IEnumerator<T1> en1, IEnumerator<T2> en2, Func<T1, T2, TResult> selector)
        {
            this.en1 = en1;
            this.en2 = en2;
            this.selector = selector;
        }

        public TResult Current => selector(en1.Current, en2.Current);
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return en1.MoveNext() && en2.MoveNext();
        }

        public void Reset()
        {
            en1.Reset();
            en2.Reset();
        }

        public void Dispose()
        {
            en1.Dispose();
            en2.Dispose();
        }
    }
}
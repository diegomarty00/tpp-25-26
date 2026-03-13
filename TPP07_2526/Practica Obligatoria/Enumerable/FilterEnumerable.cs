namespace Enumerable;

using System;
using System.Collections;
using System.Collections.Generic;

public class FilterEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> source;
    private readonly Func<T, bool> predicate;

    public FilterEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
    {
        this.source = source;
        this.predicate = predicate;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new FilterEnumerator(source.GetEnumerator(), predicate);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class FilterEnumerator : IEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly Func<T, bool> predicate;

        public FilterEnumerator(IEnumerator<T> enumerator, Func<T, bool> predicate)
        {
            this.enumerator = enumerator;
            this.predicate = predicate;
        }

        public T Current => enumerator.Current;
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                    return true;
            }
            return false;
        }

        public void Reset() => enumerator.Reset();
        public void Dispose() => enumerator.Dispose();
    }
}
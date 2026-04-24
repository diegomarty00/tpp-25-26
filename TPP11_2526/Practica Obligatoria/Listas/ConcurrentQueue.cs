using System;

namespace Listas
{
    public class ConcurrentQueue<T>
    {
        private readonly LinkedList<T> _list;
        private readonly object _sync = new object();

        public ConcurrentQueue()
        {
            _list = new LinkedList<T>();
        }

        public int Count
        {
            get
            {
                lock (_sync)
                    return _list.Count;
            }
        }
        public bool IsEmpty()
        {
            lock (_sync)
                return _list.IsEmpty();
        }

        public void Enqueue(T item)
        {
            lock (_sync)
                _list.Add(item);
        }

        public T Dequeue()
        {
            lock (_sync)
            {
                if (_list.IsEmpty())
                    throw new InvalidOperationException("La cola está vacía.");

                T value = _list.ElementAt(0);
                _list.RemoveAt(0);
                return value;
            }
        }

        public bool TryDequeue(out T valor)
        {
            lock (_sync)
            {
                if (_list.IsEmpty())
                {
                    valor = default(T);
                    return false;
                }

                valor = _list.ElementAt(0);
                _list.RemoveAt(0);
                return true;
            }
        }

        public T Peek()
        {
            lock (_sync)
            {
                if (_list.IsEmpty())
                    throw new InvalidOperationException("La cola está vacía.");

                return _list.ElementAt(0);
            }
        }

        public bool TryPeek(out T valor)
        {
            lock (_sync)
            {
                if (_list.IsEmpty())
                {
                    valor = default(T);
                    return false;
                }

                valor = _list.ElementAt(0);
                return true;
            }
        }
    }
}
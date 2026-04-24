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
                {
                    return _list.Count;
                }
            }
        }
        public bool IsEmpty()
        {
            lock (_sync)
            {
                return _list.IsEmpty();
            }
        }

        public void Enqueue(T item)
        {
            lock (_sync)
            {
                _list.Add(item);
            }
        }

        public T Dequeue()
        {
            if (!TryDequeue(out T valor))
                throw new InvalidOperationException("La cola está vacía.");

            return valor;
        }

        private bool TryDequeue(out T valor)
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
            if (!TryPeek(out T valor))
                throw new InvalidOperationException("La cola está vacía.");

            return valor;
        }

        private bool TryPeek(out T valor)
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


        public void SafeDequeue(){
            try
            {
                Console.WriteLine("Dequeue: " + Dequeue());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error en Dequeue: " + ex.Message);
            }
        }

        public void SafePeek(){
            try
            {
                Console.WriteLine("Peek: " + Peek());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error en Peek: " + ex.Message);
            }
        }
    }
}
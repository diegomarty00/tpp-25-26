using System.Collections;

namespace Listas;

public class LinkedList<T> : IEnumerable<T>
{
    private Node _head;

    public int Count { get; private set; }

    /**
    * Añade un nuevo elemento al final de la lista
    */
    public void Add(T item)
    {
        if (IsEmpty())
        {
            this._head = new Node(item, null);
        }
        else
        {
            Node last = GetNode((uint)Count - 1);
            last.Next = new Node(item, null);
        }
        this.Count++;
    }

    /**
    * Obtiene el elemento de una determinada posición.
    */
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }
        return GetNode((uint)index).Data;
    }

    /**
    * Sustituye el elemento de la posición indicada por un nuevo elemento
    */
    public void Set(int index, T item)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }
        Node node = GetNode((uint)index);
        node.Data = item;
    }

    /**
    * Añade un nuevo elemento en la posición indicada
    */
    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }
        if (index == 0)
        {
            _head = new Node(item, _head);
        }
        else
        {
            Node previous = GetNode((uint)(index - 1));
            previous.Next = new Node(item, previous.Next);
        }
        Count++;
    }

    /**
    * Comprueba si la lista contiene el objeto que se le pasa como parámetro
    */
    public bool Contains(T item)
    {
        Node? current = _head;
        while (current != null)
        {
            if (current.Data == null && item == null)
                return true;
            if (current.Data != null && current.Data.Equals(item))
                return true;
            current = current.Next;
        }
        return false;
    }

    /**
    *  Busca si el objeto que se le pasa como parámetro está en la lista. 
    *  Si lo encuentra, lo elimina y devuelve true. 
    *  Si no lo encuentra, devuelve false
    */
    public bool Remove(T item)
    {
        if (IsEmpty())
            return false;

        // Caso especial: eliminar la cabeza
        if ((_head.Data == null && item == null) ||
            (_head.Data != null && _head.Data.Equals(item)))
        {
            _head = _head.Next;
            Count--;
            return true;
        }

        Node current = _head;

        while (current.Next != null)
        {
            if ((current.Next.Data == null && item == null) ||
                (current.Next.Data != null && current.Next.Data.Equals(item)))
            {
                current.Next = current.Next.Next;
                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }


    /**
    * Elimina el elemento en la posición indicada.
    */
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }
        if (index == 0)
        {
            _head = _head.Next;
        }
        else
        {
            Node previous = GetNode((uint)(index - 1));
            previous.Next = previous.Next.Next;
        }
        Count--;
    }

    /**
    * Elimina todos los elementos de la lista
    */
    public void Clear()
    {
        _head = null;
        Count = 0;
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }

    private Node GetNode(uint index)
    {
        uint currentIndex = 0;
        Node currentNode = _head;
        while (currentIndex < index)
        {
            currentIndex++;
            currentNode = currentNode.Next;
        }
        return currentNode;
    }

    private class Node
    {
        public T? Data { get; set; }
        public Node? Next { get; set; }

        public Node(T? data, Node? next)
        {
            this.Data = data;
            this.Next = next;
        }
    }


    // Implementación de IEnumerable<T>
    
    public IEnumerator<T> GetEnumerator()
    {
        return new LinkedListEnumerator(_head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    private class LinkedListEnumerator : IEnumerator<T>
    {
        private Node _head;
        private Node? _current;

        public LinkedListEnumerator(Node head)
        {
            _head = head;
            _current = null;
        }

        public T Current => _current.Data;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_current == null)
                _current = _head;
            else
                _current = _current.Next;

            return _current != null;
        }

        public void Reset()
        {
            _current = null;
        }

        public void Dispose() { }
    }

}

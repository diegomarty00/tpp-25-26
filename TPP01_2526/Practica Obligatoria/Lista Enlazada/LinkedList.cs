namespace Lista_Enlazada;

public class LinkedList
{
    private Node _head;

    public int Count { get; private set; }

    /**
    * Añade un nuevo elemento al final de la lista
    */
    public void Add(Object item)
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
    public Object ElementAt(int index)
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
    public void Set(int index, Object item)
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
    public void Insert(int index, Object item)
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
    public bool Contains(Object item)
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
    public bool Remove(Object item)
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
        public Object? Data { get; set; }
        public Node? Next { get; set; }

        public Node(Object? data, Node? next)
        {
            this.Data = data;
            this.Next = next;
        }
    }
}

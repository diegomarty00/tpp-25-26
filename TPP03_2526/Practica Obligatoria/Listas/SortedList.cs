namespace Listas;

public class SortedList
{
    private LinkedList List  { get;  set; }
    public int Count
    {
        get
        {
            return List.Count;
        }
        private set { }
    }

    public SortedList()
    {
        List = new LinkedList();
    }

    public void Add(IComparable item)
    {

        if (item is null)
            throw new ArgumentNullException();

        if (IsEmpty())
        {
            List.Add(item);
        }
        else
        {
            for (int i = 0; i < Count; i++)
            {
                if (CompareTo(item, (IComparable)List.ElementAt(i)) > 0)
                {
                    Insert(i, item);
                    return;
                }
            }
            List.Add(item);
        }
    }

    private void Insert(int index, Object item)
    {
        if (index < 0 || index > Count)
            throw new IndexOutOfRangeException();

        var newList = new LinkedList();

        for (int i = 0; i < index; i++)
            newList.Add(List.ElementAt(i));

        newList.Add(item);

        for (int i = index; i < Count; i++)
            newList.Add(List.ElementAt(i-1));

        List = newList;
    }

    public object ElementAt(int index)
    {
        return List.ElementAt(index);
    }

    public bool Contains(Object item)
    {
        return List.Contains(item);
    }

    public bool Remove(Object item)
    {
        return List.Remove(item);
    }

    public void RemoveAt(int index)
    {
        List.RemoveAt(index);
    }

    public void Clear()
    {
        List.Clear();
    }

    public bool IsEmpty()
    {
        return List.IsEmpty();
    }

    private static int CompareTo(IComparable a, IComparable b)
    {
        try
        {
            return a.CompareTo(b);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException(
                "Los elementos deben ser comparables entre sí (mismo tipo o tipos compatibles para CompareTo).");
        }
    }

}


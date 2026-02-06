namespace Listas;

public class SortedList
{
    private LinkedList<IComparable> _list;
    public int Count { get; private set; }

    public SortedList()
    {
        _list = new LinkedList<IComparable>();
    }

    public void Add(IComparable item)
    {
        _list.Add(item);
        Count++;
    }

    private int CompareTo(Object other)
    {
        if (other is SortedList otherList)
        {
            return Count.CompareTo(otherList.Count);
        }
        throw new ArgumentException("Object is not a SortedList");
    }
}

interface IComparable<Object>
{
}

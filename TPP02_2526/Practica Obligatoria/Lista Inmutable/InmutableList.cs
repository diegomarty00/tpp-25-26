namespace Lista_Inmutable;

public class InmutableList
{

    public int Count { get; private set; }
    private Object[] _list;

    /// <summary>
    /// Constructor de la lista inmutable
    /// </summary>
    /// <param name="items">Array de objetos a incluir en la lista</param>
    public InmutableList(Object[]? items)
    {
        this.Count = items?.Length ?? 0;
        this._list = new Object[this.Count];

        if (items != null)
            for (int i = 0; i < this.Count; i++)
                this._list[i] = items[i];
    }

    /// <summary>
    /// Añade un nuevo elemento al final de la lista
    /// </summary>
    /// <param name="item">Elemento a añadir</param>
    /// <returns>Nueva lista con el elemento añadido</returns>
    public InmutableList Add(Object item)
    {
        Object[] newList = new Object[this.Count + 1];
        for (int i = 0; i < this.Count; i++)
        {
            newList[i] = this._list[i];
        }
        newList[this.Count] = item;
        return new InmutableList(newList);
    }

    /// <summary>
    /// Obtiene el elemento de una determinada posición.
    /// </summary>
    /// <param name="index">Posición del elemento a obtener</param>
    /// <returns>Elemento en la posición indicada</returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public Object ElementAt(int index)
    {
        if (index < 0 || index >= this.Count)
            throw new IndexOutOfRangeException();

        return this._list[index];
    }


    /// <summary>
    /// Sustituye el elemento de la posición indicada por un nuevo elemento
    /// </summary>
    /// <param name="index">Posición del elemento a sustituir</param>
    /// <param name="item">Nuevo elemento</param>
    /// <returns>Nueva lista con el elemento sustituido</returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public InmutableList Set(int index, Object item)
    {
        if (index < 0 || index >= this.Count)
            throw new IndexOutOfRangeException();

        Object[] newList = new Object[this.Count];

        for (int i = 0; i < this.Count; i++)
            newList[i] = this._list[i];

        newList[index] = item;
        return new InmutableList(newList);
    }


    /// <summary>
    /// Añade un nuevo elemento en la posición indicada
    /// </summary>
    /// <param name="index">Posición donde se añadirá el elemento</param>
    /// <param name="item">Elemento a añadir</param>
    /// <returns>Nueva lista con el elemento añadido</returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public InmutableList Insert(int index, Object item)
    {
        if (index < 0 || index > this.Count)
            throw new IndexOutOfRangeException();

        Object[] newList = new Object[this.Count + 1];

        for (int i = 0; i < index; i++)
            newList[i] = this._list[i];

        newList[index] = item;

        for (int i = index; i < this.Count; i++)
            newList[i + 1] = this._list[i];

        return new InmutableList(newList);
    }

    /// <summary>
    /// Comprueba si la lista contiene un elemento
    /// </summary>
    /// <param name="item">Elemento a buscar</param>
    /// <returns>
    /// True --> el elemento está en la lista
    /// False --> en caso contrario
    /// </returns>
    public bool Contains(Object item)
    {
        for (int i = 0; i < this.Count; i++)
            if (this._list[i].Equals(item))
                return true;
        return false;
    }


/// <summary>
///  Busca si el objeto que se le pasa como parámetro está en la lista.
/// </summary>
/// <param name="item">Elemento a buscar</param>
/// <returns>Nueva lista sin el elemento indicado</returns>
    public InmutableList Remove(object item)
    {
        int occurrences = 0;
        for (int i = 0; i < this.Count; i++)
            if (this._list[i].Equals(item))
                occurrences++;

        Object[] newList = new Object[this.Count - occurrences];
        int newIndex = 0;

        for (int i = 0; i < this.Count; i++)
        {
            if (!this._list[i].Equals(item))
            {
                newList[newIndex] = this._list[i];
                newIndex++;
            }
        }

        return new InmutableList(newList);
    }


/// <summary>
///  Elimina el elemento en la posición indicada
/// </summary>
/// <param name="index">Posición del elemento a eliminar</param>
/// <returns>Nueva lista sin el elemento indicado</returns>
/// <exception cref="IndexOutOfRangeException"></exception>
    public InmutableList RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
            throw new IndexOutOfRangeException();

        Object[] newList = new Object[this.Count - 1];
        int newIndex = 0;

        for (int i = 0; i < this.Count; i++)
        {
            if (i != index)
            {
                newList[newIndex] = this._list[i];
                newIndex++;
            }
        }

        return new InmutableList(newList);
    }

    /// <summary>
    /// Elimina todos los elementos de la lista
    /// </summary>
    /// <returns>Nueva lista vacía</returns>
    public InmutableList Clear()
    {
        return new InmutableList(new Object[0]);
    }

}

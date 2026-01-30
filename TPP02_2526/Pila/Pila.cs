namespace Pila;

public class Pila
{
    public int Capacidad { get; init; }

    public int Count { get; private set; } = 0;

    int [] Elementos { get; set; }

    public Pila(){}

    public Pila(int capacidad)
    {
        Capacidad = capacidad;
        Elementos = new int[Capacidad];
    }


    public void Push(int elemento)
    {
        if (Count >= Capacidad)
            throw new InvalidOperationException("Pila llena");

        Count += 1;
        Elementos[Count - 1] = elemento;
    }

    public int Pop()
    {
        if (Count <= 0)
            throw new InvalidOperationException("Pila vacía");

        int elemento = Elementos[Count - 1];
        Count -= 1;
        return elemento;
    }

    public bool Contains(int elemento)
    {
        for (int i = 0; i < Count; i++)
        {
            if (Elementos[i] == elemento)
            {
                return true;
            }
        }
        return false;
    }


}

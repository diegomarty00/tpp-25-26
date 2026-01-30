namespace Console_App;
using Lista_Enlazada;
using Personas;
class Program
{
    static void Main(string[] args)
    {
        LinkedList list = new LinkedList();

        Console.WriteLine("=== DEMOSTRACIÓN DE LINKEDLIST ===\n");

        // Añadir elementos de distintos tipos
        list.Add(42);                     // int
        list.Add("Hola mundo");           // string
        list.Add(new Client("Ana", 30));  // Client
        list.Add(null);                   // null permitido

        Console.WriteLine("Elementos añadidos:");
        PrintList(list);

        // Insertar en una posición concreta
        list.Insert(1, "Insertado en posición 1");
        Console.WriteLine("\nDespués de insertar un string en la posición 1:");
        PrintList(list);

        // Obtener un elemento
        Console.WriteLine($"\nElemento en la posición 2: {list.ElementAt(2)}");

        // Modificar un elemento
        list.Set(0, 99);
        Console.WriteLine("\nDespués de modificar la posición 0:");
        PrintList(list);

        // Comprobar Contains
        Console.WriteLine($"\n¿La lista contiene 'Hola mundo'? {list.Contains("Hola mundo")}");
        Console.WriteLine($"¿La lista contiene null? {list.Contains(null)}");

        // Eliminar un elemento
        list.Remove("Hola mundo");
        Console.WriteLine("\nDespués de eliminar 'Hola mundo':");
        PrintList(list);

        // Eliminar por índice
        list.RemoveAt(0);
        Console.WriteLine("\nDespués de eliminar el elemento en la posición 0:");
        PrintList(list);

        // Limpiar la lista
        list.Clear();
        Console.WriteLine("\nDespués de limpiar la lista:");
        PrintList(list);

        Console.WriteLine("\n=== FIN DE LA DEMOSTRACIÓN ===");
    }

    static void PrintList(LinkedList list)
    {
        Console.WriteLine($"Count = {list.Count}");

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"[{i}] -> {list.ElementAt(i)}");
        }
    }
}

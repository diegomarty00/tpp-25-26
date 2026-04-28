using System;
using Listas;

namespace Console_App
{

    class Program
    {
        static void Main(string[] args)
        {
            var cola = new ConcurrentQueue<int>();

            Console.WriteLine("=== Pruebas básicas ===");

            cola.Enqueue(10);
            cola.Enqueue(20);
            cola.Enqueue(30);

            Console.WriteLine($"Count: {cola.Count}");

            Console.WriteLine($"Peek: {cola.Peek()}");

            Console.WriteLine($"Dequeue: {cola.Dequeue()}");
            Console.WriteLine($"Dequeue: {cola.Dequeue()}");

            Console.WriteLine($"¿Está vacía?: {cola.IsEmpty()}");
            
            if (cola.TryDequeue(out int valor))
                Console.WriteLine($"TryDequeue obtuvo: {valor}");
            else
                Console.WriteLine("TryDequeue falló");

            if (cola.TryDequeue(out int valor2))
                Console.WriteLine($"TryDequeue obtuvo: {valor2}");
            else
                Console.WriteLine("TryDequeue falló");

            Console.WriteLine($"¿Está vacía?: {cola.IsEmpty()}");

        }
    }
}



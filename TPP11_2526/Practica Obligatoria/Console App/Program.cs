using System;
using Listas;

namespace Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new ConcurrentQueue<int?>();

            queue.SafePeek();
            queue.SafeDequeue();

            queue.Enqueue(1);       // Se añade = 1
            queue.SafePeek();
            
            queue.SafeDequeue();    // Se elimina = 0
            queue.SafeDequeue();

            queue.Enqueue(2);       // Se añade = 1
            queue.SafePeek();
            queue.Enqueue(3);       // Se añade = 2
            queue.Enqueue(null);    // Se añade = 3

            Console.WriteLine("Elementos encolados: "+ queue.Count);

        }
    }
}
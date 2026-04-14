namespace Console_App;

using Listas;

class Program
{
    static void Main(string[] args)
    {

        var queue = new ConcurrentQueue<int?>();

        // Productores
        Task producer1 = Task.Run(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                queue.Enqueue(i);
                Console.WriteLine($"Enqueue: {i}");
            }
        });

        Task producer2 = Task.Run(() =>
        {
            queue.Enqueue(null);
            Console.WriteLine("Enqueue: null");
        });

        // Consumidor
        Task consumer = Task.Run(() =>
        {
            int removed = 0;
            while (removed < 6)
            {
                if (!queue.IsEmpty())
                {
                    var value = queue.Dequeue();
                    Console.WriteLine($"Dequeue: {value}");
                    removed++;
                }
            }
        });

        Task.WaitAll(producer1, producer2, consumer);

        Console.WriteLine("Finalización correcta.");

    }
}

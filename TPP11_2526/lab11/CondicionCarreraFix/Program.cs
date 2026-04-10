using System.Diagnostics;

namespace CondicionCarreraFix;

class Program
{

    static short[] vector = CrearVectorAleatorio(20000000, 0, 10);
    static int numHilos = 4;
    static void Main()
    {
        DateTime before = DateTime.Now;
        BusquedaSecuencial();
        DateTime after = DateTime.Now;
        Console.WriteLine("BusquedaSecuencial time: {0:N0} ticks.",
                (after - before).Ticks);

        before = DateTime.Now;
        BusquedaMultihiloLock();
        after = DateTime.Now;
        Console.WriteLine("BusquedaMultihiloLock time: {0:N0} ticks.",
                (after - before).Ticks);

        before = DateTime.Now;
        BusquedaMultihiloInterlocked();
        after = DateTime.Now;
        Console.WriteLine("BusquedaMultihiloInterlocked time: {0:N0} ticks.",
                (after - before).Ticks);
        // EJERCICIO: Implementa la solución óptima.

        before = DateTime.Now;
        BusquedaMultihiloVariable();
        after = DateTime.Now;
        Console.WriteLine("BusquedaMultihiloVariable time: {0:N0} ticks.",
                (after - before).Ticks);

    }

    public static void BusquedaMultihiloLock()
    {

        object syncLock = new object();

        int recuentoMultihilo = 0;
        Thread[] hilos = new Thread[numHilos];
        for (int i = 0; i < hilos.Length; i++)
        {
            int inicio = i * vector.Length / hilos.Length;
            int fin = inicio + vector.Length / hilos.Length;
            if (i == hilos.Length - 1)
                fin = vector.Length;

            hilos[i] = new Thread(() =>
            {
                for (int i = inicio; i < fin; i++)
                {
                    if (vector[i] is 2 or 3)
                    {
                        // Sección crítica y exclusión mutua
                        // ¿Cómo funciona el lock?
                        lock (syncLock)
                        {
                            recuentoMultihilo++;
                        }

                    }
                }
            });
            hilos[i].Start();
        }

        foreach (var hilo in hilos)
            hilo.Join();

        Console.WriteLine($"[Multihilo (Lock)] Los números 2 y 3 aparecen {recuentoMultihilo} veces.");
    }

    public static void BusquedaMultihiloInterlocked()
    {
        // Interlocked (System.Threading) -> Operaciones atómicas primitivas.
        // Como norma general, es más eficiente que lock.
        // Métodos más utilizados: Increment, Decrement, Add y Exchange.
        // https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-9.0
        int recuentoMultihilo = 0;
        Thread[] hilos = new Thread[numHilos];
        for (int i = 0; i < hilos.Length; i++)
        {
            int inicio = i * vector.Length / hilos.Length;
            int fin = inicio + vector.Length / hilos.Length;
            if (i == hilos.Length - 1)
                fin = vector.Length;

            hilos[i] = new Thread(() =>
            {
                for (int j = inicio; j < fin; j++)
                {
                    if (vector[j] is 2 or 3)
                    {
                        Interlocked.Increment(ref recuentoMultihilo);
                    }
                }
            });
            hilos[i].Start();
        }

        foreach (var hilo in hilos)
            hilo.Join();

        Console.WriteLine($"[Multihilo (Interlocked)] Los números 2 y 3 aparecen {recuentoMultihilo} veces.");
    }

    public static void BusquedaMultihiloVariable()
    {
        Thread[] hilos = new Thread[numHilos];
        int[] parciales = new int[numHilos];

        for (int i = 0; i < numHilos; i++)
        {
            int hiloIndex = i;

            int inicio = i * vector.Length / numHilos;
            int fin = inicio + vector.Length / numHilos;
            if (i == numHilos - 1)
                fin = vector.Length;

            hilos[i] = new Thread(() =>
            {
                int recuentoLocal = 0;

                for (int j = inicio; j < fin; j++)
                {
                    if (vector[j] is 2 or 3)
                        recuentoLocal++;
                }
                parciales[hiloIndex] = recuentoLocal;
            });

            hilos[i].Start();
        }

        foreach (var hilo in hilos)
            hilo.Join();

        int resultado = 0;
        foreach (var parcial in parciales)
            resultado += parcial;

        Console.WriteLine($"[Multihilo (Variable)] Los números 2 y 3 aparecen {resultado} veces.");
    }



    public static void BusquedaSecuencial()
    {
        int recuentoSecuencial = 0;
        for (int i = 0; i < vector.Length; i++)
        {
            if (vector[i] is 2 or 3)
                recuentoSecuencial++;
        }
        Console.WriteLine($"[Secuencial] Los números 2 y 3 aparecen {recuentoSecuencial} veces.");
    }

    public static short[] CrearVectorAleatorio(int numElementos, short menor, short mayor)
    {
        short[] vector = new short[numElementos];
        Random random = new Random();
        for (int i = 0; i < numElementos; i++)
            vector[i] = (short)random.Next(menor, mayor + 1);
        return vector;
    }


}

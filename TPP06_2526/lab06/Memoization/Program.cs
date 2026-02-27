﻿using System.Diagnostics;
using System.Security.Cryptography;

namespace lab06;
class Program
{
    static void Main()
    {
        const int N = 40;
        var crono = new Stopwatch();

        crono.Start();
        var res1 = Fibonacci(N);
        crono.Stop();
        var ticks1 = crono.ElapsedTicks;
        Console.WriteLine($"Tiempo: {ticks1} ticks. Resultado: {res1}");

        crono.Restart();
        var res2 = Fibonacci(N);
        crono.Stop();
        var ticks2 = crono.ElapsedTicks;
        Console.WriteLine($"Tiempo: {ticks2} ticks. Resultado: {res2}");

        var memoizedFibonacci = ObtenerMemoizedFibonacci();

        crono.Restart();
        var res3 = memoizedFibonacci(N);
        crono.Stop();
        var ticks3 = crono.ElapsedTicks;
        Console.WriteLine($"Tiempo: {ticks3} ticks. Resultado: {res3}");

        crono.Restart();
        var res4 = memoizedFibonacci(N);
        crono.Stop();
        var ticks4 = crono.ElapsedTicks;
        Console.WriteLine($"Tiempo: {ticks4} ticks. Resultado: {res4}");
    }

    static int Fibonacci(int n)
    {
        return n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);
    }

    static Func<int, int> ObtenerMemoizedFibonacci()
    {
        var valores = new Dictionary<int, int>();
        int Fibonacci(int n)
        {
            if (valores.ContainsKey(n)) return valores[n];

            var res = n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);

            valores[n] = res;
            return res;
        }
        return Fibonacci;
    }

    /***************************************************************************
     * ¿Tiene sentido memoizar estas funciones?
     ***/
     // No es determinista, no es puro, no es referencialmente transparente, no es idempotente, no es inmutable, no es thread-safe
    static int SimulacionCalculoCosteAlto(int x, int y, int z)
    {
        // Simulamos una operación de coste alto
        Thread.Sleep(1000);

        return Random.Shared.Next(1, x + y + z + 1);
    }

    static int counter = 0;
    // No es determinista, no es puro, no es referencialmente transparente, no es idempotente, no es inmutable, no es thread-safe
    static int SimulacionProcesoCalculoCosteAlto(int x, int y, int z)
    {
        // Simulamos una operación de coste alto
        Thread.Sleep(1000);

        counter += x + y + z; // Esto es un efecto secundario que hace que la función no sea pura ni referencialmente transparente, además de no ser idempotente ni inmutable.
        return x + y + z;
    }

    // No es determinista, no es puro, no es referencialmente transparente, no es idempotente, no es inmutable, no es thread-safe
    static int CalcularConLog(int x, int y, int z)
    {
        // Simulamos una operación de coste alto
        Thread.Sleep(1000);

        int r = x + y + z;
        File.AppendAllText("audit.log", $"Calculado {r} en {DateTime.Now:O}\n"); // Esto es un efecto secundario que hace que la función no sea pura ni referencialmente transparente, además de no ser idempotente ni inmutable.
        return r;
    }

    // No es determinista, no es puro, no es referencialmente transparente, no es idempotente, no es inmutable, no es thread-safe
    static int FuncionDeterministaEntradaMutableCosteAlto(List<int> xs)
    {
        // Simulamos una operación de coste alto
        Thread.Sleep(1000);
        return xs.Sum(); // Aunque el resultado es siempre el mismo para la misma lista, la función no es pura ni referencialmente transparente porque la lista es mutable y puede cambiar entre llamadas, lo que afectaría al resultado. Además, no es idempotente ni inmutable.
    }

    // No es determinista, no es puro, no es referencialmente transparente, no es idempotente, no es inmutable, no es thread-safe
    static string HashFile(string path)
    {
        using var stream = File.OpenRead(path); 
        using var sha = SHA256.Create(); 
        return Convert.ToHexString(sha.ComputeHash(stream));
    }
}

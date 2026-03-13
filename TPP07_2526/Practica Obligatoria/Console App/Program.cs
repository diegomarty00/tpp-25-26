using System;
using System.Collections.Generic;
using Lazy;
using Enumerable;

class MainProgram
{
    static void Main()
    {
        var nums = new[] { 1, 2, 3, 4, 5 };
        var letras = new[] { "A", "B", "C" };

        // ======================================================
        // 1. PRUEBAS GENERADORES (yield return)
        // ======================================================

        Console.WriteLine("=====================================");
        Console.WriteLine(" PRUEBAS: MÉTODOS BASADOS EN GENERADORES");
        Console.WriteLine("=====================================");

        // ---- Map ----
        Console.WriteLine("\nMap (x2):");
        foreach (var x in nums.MapLazy(n => n * 2))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- Filter ----
        Console.WriteLine("\nFilter (pares):");
        foreach (var x in nums.FilterLazy(n => n % 2 == 0))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- Zip ----
        Console.WriteLine("\nZipLazy:");
        foreach (var x in nums.ZipLazy(letras, (n, s) => $"{n}-{s}"))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- Take ----
        Console.WriteLine("\nTakeLazy(3):");
        foreach (var x in nums.TakeLazy(3))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- TakeWhile ----
        Console.WriteLine("\nTakeWhileLazy (n < 4):");
        foreach (var x in nums.TakeWhileLazy(n => n < 4))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- Skip ----
        Console.WriteLine("\nSkipLazy(2):");
        foreach (var x in nums.SkipLazy(2))
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- SkipWhile ----
        Console.WriteLine("\nSkipWhileLazy (n < 3):");
        foreach (var x in nums.SkipWhileLazy(n => n < 3))
            Console.Write($"{x} ");
        Console.WriteLine();


        // ======================================================
        // 2. PRUEBAS ENUMERADORES MANUALES (IEnumerable<T>)
        // ======================================================

        Console.WriteLine("\n\n=====================================");
        Console.WriteLine(" PRUEBAS: IMPLEMENTACIONES MANUALES DE IEnumerable");
        Console.WriteLine("=====================================");

        // ---- MapEnumerable ----
        Console.WriteLine("\nMapEnumerable (x3):");
        var mapEnum = new MapEnumerable<int, int>(nums, n => n * 3);
        foreach (var x in mapEnum)
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- FilterEnumerable ----
        Console.WriteLine("\nFilterEnumerable (impares):");
        var filterEnum = new FilterEnumerable<int>(nums, n => n % 2 == 1);
        foreach (var x in filterEnum)
            Console.Write($"{x} ");
        Console.WriteLine();

        // ---- ZipEnumerable ----
        Console.WriteLine("\nZipEnumerable:");
        var zipEnum = new ZipEnumerable<int, string, string>(
            nums,
            letras,
            (n, s) => $"({n},{s})"
        );
        foreach (var x in zipEnum)
            Console.Write($"{x} ");
        Console.WriteLine();

        Console.WriteLine("\n\nFin de las pruebas");
        Console.WriteLine("=====================================");
    }
}
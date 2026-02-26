namespace Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transacciones;

[TestClass]
public sealed class FuncionesOrdenSuperiorTests
{
    // ---------- MAP ----------
    [TestMethod]
    public void Map_MismoTipo_CuadradoDeEnteros()
    {
        var origen = new[] { 1, 2, 3, 4 };
        var res = Program.Map(origen, x => x * x);
        CollectionAssert.AreEqual(new[] { 1, 4, 9, 16 }, res.ToArray());
    }
    [TestMethod]
    public void Map_TipoDestinoDiferente_ContarVocales()
    {
        var origen = new[] { "hello", "mundo" };
        Func<string, int> contarVocales = s =>
            s.Count(c => "aeiouáéíóúAEIOUÁÉÍÓÚ".Contains(c));
        var res = Program.Map(origen, contarVocales).ToArray();
        // "hello" -> e,o (2); "mundo" -> u,o (2)
        CollectionAssert.AreEqual(new[] { 2, 2 }, res);
    }

    // ---------- FILTER ----------
    [TestMethod]
    public void Filter_TodosCumplen_DevuelveTodos()
    {
        var origen = new[] { 2, 4, 6 };
        var res = Program.Filter(origen, x => x % 2 == 0);
        CollectionAssert.AreEqual(origen, res.ToArray());
    }

    [TestMethod]
    public void Filter_AlgunosCumplen_SoloLosQueCumplen()
    {
        var origen = new[] { 1, 2, 3, 4, 5 };
        var res = Program.Filter(origen, x => x % 2 == 0);
        CollectionAssert.AreEqual(new[] { 2, 4 }, res.ToArray());
    }

    [TestMethod]
    public void Filter_NadieCumple_DevuelveVacio()
    {
        var origen = new[] { 1, 3, 5 };
        var res = Program.Filter(origen, x => x % 2 == 0);
        Assert.AreEqual(0, res.Count());
    }

    // ---------- REDUCE ----------
    [TestMethod]
    public void Reduce_MismoTipo_Sumatorio()
    {
        var origen = new[] { 1, 2, 3, 4 };
        var suma = Program.Reduce<int, int>(origen, (elem, acc) => acc + elem, 0);
        Assert.AreEqual(10, suma);
    }

    [TestMethod]
    public void Reduce_MismoTipo_ConSemilla_Minimo()
    {
        var origen = new[] { 5, 2, 8, 1 };
        // Nota: semilla  int -> usamos int.MaxValue para evitar el caso especial del 0
        var min = Program.Reduce<int, int>(origen, (elem, acc) => Math.Min(acc, elem), int.MaxValue);
        Assert.AreEqual(1, min);
    }

    [TestMethod]
    public void Reduce_TipoDestinoDiferente_SumatorioLongitudes()
    {
        var origen = new[] { "hola", "adiós" };
        var totalLen = Program.Reduce<string, int>(origen, (s, acc) => acc + s.Length, 0);
        Assert.AreEqual("hola".Length + "adiós".Length, totalLen);
    }

    [TestMethod]
    public void Reduce_TipoDestinoDiferente_ConSemilla_DiccionarioVocales()
    {
        var origen = new[] { "hello", "mundo" };
        var seed = new Dictionary<char, int>();
        var dic = Program.Reduce<string, Dictionary<char,int>>(origen, (s, acc) =>
        {
            foreach (var c in s.ToLowerInvariant())
                if ("aeiouáéíóú".Contains(c))
                    acc[c] = acc.TryGetValue(c, out var n) ? n + 1 : 1;
            return acc;
        }, seed);

        Assert.IsTrue(dic.Values.Sum() >= 1);
        Assert.IsTrue(dic.ContainsKey('e'));
        Assert.IsTrue(dic.ContainsKey('o'));
    }

    // ---------- ZIP ----------
    [TestMethod]
    public void Zip_MismoTipo_MismaLongitud()
    {
        var a = new[] { 1, 2, 3 };
        var b = new[] { 10, 20, 30 };
        // Asegúrate de hacer Program.Zip(...) público
        var z = Program.Zip(a, b, (x, y) => x + y).ToArray();
        CollectionAssert.AreEqual(new[] { 11, 22, 33 }, z);
    }

    [TestMethod]
    public void Zip_TiposYLongitudesDiferentes_TomaLongitudMasCorta()
    {
        var a = new[] { "A", "B", "C" };
        var b = new[] { 1m, 2m };
        var z = Program.Zip(a, b, (x, y) => (x, y)).ToArray();
        Assert.AreEqual(2, z.Length);
        Assert.AreEqual(("A", 1m), z[0]);
        Assert.AreEqual(("B", 2m), z[1]);
    }

    // ---------- COMBINACIONES ----------
    [TestMethod]
    public void Combo_MapFilterReduce_SumaCuadradosDePares()
    {
        var origen = new[] { 1, 2, 3, 4, 5 };
        var pares = Program.Filter(origen, x => x % 2 == 0);
        var cuadrados = Program.Map(pares, x => x * x);
        var suma = Program.Reduce<int, int>(cuadrados, (x, acc) => acc + x, 0);
        Assert.AreEqual(20, suma);
    }
}

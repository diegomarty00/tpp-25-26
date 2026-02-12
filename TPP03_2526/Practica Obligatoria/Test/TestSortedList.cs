namespace Test;

using System;
using Listas;
using Microsoft.VisualStudio.TestTools.UnitTesting;



[TestClass]
public sealed class SortedListTests
{
    private SortedList? lista;

    [TestMethod]
    public void TestConstructor()
    {
        this.lista = new SortedList();
        Assert.AreEqual(0, this.lista.Count);
        Assert.IsTrue(this.lista.IsEmpty()); 
    }

    [TestMethod]
    public void TestAdd_MantieneOrden_Y_DuplicadosDespues()
    {
        this.lista = new SortedList();
        this.lista.Add(5);
        this.lista.Add(3);
        this.lista.Add(7);
        this.lista.Add(1);
        this.lista.Add(3); // duplicado: debe quedar justo después del 3 ya existente

        Assert.AreEqual(5, this.lista.Count);
        Assert.AreEqual(1, this.lista.ElementAt(0));
        Assert.AreEqual(3, this.lista.ElementAt(1));
        Assert.AreEqual(3, this.lista.ElementAt(2)); // duplicado detrás
        Assert.AreEqual(5, this.lista.ElementAt(3));
        Assert.AreEqual(7, this.lista.ElementAt(4));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAdd_NoPermiteNull()
    {
        this.lista = new SortedList();
        this.lista.Add((IComparable)null!);
    }

    [TestMethod]
    public void TestElementAt()
    {
        this.lista = new SortedList();
        this.lista.Add("c");
        this.lista.Add("a");
        this.lista.Add("b");
        // Orden esperado: "a", "b", "c"
        Assert.AreEqual("a", this.lista.ElementAt(0));
        Assert.AreEqual("b", this.lista.ElementAt(1));
        Assert.AreEqual("c", this.lista.ElementAt(2));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void TestElementAt_IndexOutOfRangeException()
    {
        this.lista = new SortedList();
        this.lista.Add(1);
        var _ = this.lista.ElementAt(1); // índice fuera de rango
    }

    [TestMethod]
    public void TestContains()
    {
        this.lista = new SortedList();
        this.lista.Add("b");
        this.lista.Add("a");
        this.lista.Add("c");

        Assert.IsTrue(this.lista.Contains("a"));
        Assert.IsTrue(this.lista.Contains("b"));
        Assert.IsTrue(this.lista.Contains("c"));
        Assert.IsFalse(this.lista.Contains("d"));

        // Opción (ya que Add no admite null): Contains(null) debería ser false
        Assert.IsFalse(this.lista.Contains(null));
    }

    [TestMethod]
    public void TestRemove_EliminaPrimeraCoincidencia()
    {
        this.lista = new SortedList();
        this.lista.Add("b");
        this.lista.Add("a");
        this.lista.Add("b");
        this.lista.Add("c");
        // Orden: a, b, b, c

        var removed = this.lista.Remove("b"); // quita la primera 'b'
        Assert.IsTrue(removed);
        Assert.AreEqual(3, this.lista.Count);
        Assert.AreEqual("a", this.lista.ElementAt(0));
        Assert.AreEqual("b", this.lista.ElementAt(1));
        Assert.AreEqual("c", this.lista.ElementAt(2));

        // Eliminar elemento inexistente -> false
        Assert.IsFalse(this.lista.Remove("x"));
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        this.lista = new SortedList();
        this.lista.Add("c");
        this.lista.Add("a");
        this.lista.Add("b");
        // Orden: a, b, c

        this.lista.RemoveAt(1); // quita 'b'
        Assert.AreEqual(2, this.lista.Count);
        Assert.AreEqual("a", this.lista.ElementAt(0));
        Assert.AreEqual("c", this.lista.ElementAt(1));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void TestRemoveAt_IndexOutOfRangeException()
    {
        this.lista = new SortedList();
        this.lista.Add(10);
        this.lista.RemoveAt(1); // fuera de rango
    }

    [TestMethod]
    public void TestClear()
    {
        this.lista = new SortedList();
        this.lista.Add(2);
        this.lista.Add(1);
        this.lista.Add(3);
        this.lista.Clear();
        Assert.AreEqual(0, this.lista.Count);
        Assert.IsTrue(this.lista.IsEmpty());
    }

    [TestMethod]
    public void TestMezclaDeTiposNoComparables_LanzaExcepcionEnAdd()
    {
        this.lista = new SortedList();
        this.lista.Add("a");
        // string vs int no son comparables entre sí vía CompareTo → debe fallar
        Assert.ThrowsException<ArgumentException>(() => this.lista.Add(5));
    }
}

namespace Test;

using Lista_Inmutable;
[TestClass]
public sealed class TestListaInmutable
{

    private InmutableList? lista;

    [TestMethod]
    public void TestConstructor()
    {
        this.lista = new InmutableList();
        Assert.AreEqual(0, this.lista.Count);
    }

    [TestMethod]
    public void TestAdd()
    {
        this.lista = new InmutableList();
        var nuevaLista = this.lista.Add(1);
        Assert.AreEqual(0, this.lista.Count);
        Assert.AreEqual(1, nuevaLista.Count);
        Assert.AreEqual(1, nuevaLista.ElementAt(0));
    }

    [TestMethod]
    public void TestElementAt()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        Assert.AreEqual("a", this.lista.ElementAt(0));
        Assert.AreEqual("b", this.lista.ElementAt(1));
        Assert.AreEqual("c", this.lista.ElementAt(2));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void TestElementAt_IndexOutOfRangeException()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var elemento = this.lista.ElementAt(3); // Índice fuera de rango
    }

    [TestMethod]
    public void TestSet()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var nuevaLista = this.lista.Set(1, "x");
        Assert.AreEqual("a", nuevaLista.ElementAt(0));
        Assert.AreEqual("x", nuevaLista.ElementAt(1));
        Assert.AreEqual("c", nuevaLista.ElementAt(2));
        // Verificar que la lista original no ha cambiado
        Assert.AreEqual("b", this.lista.ElementAt(1));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void TestSet_IndexOutOfRangeException()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var nuevaLista = this.lista.Set(3, "x"); // Índice fuera de rango
    }

    [TestMethod]
    public void TestToString()
    {
        this.lista = new InmutableList(new Object?[] { "a", null, "c" });
        var str = this.lista.ToString();
        Assert.AreEqual("InmutableList(a, null, c)", str);
    }

    [TestMethod]
    public void TestContains()
    {
        this.lista = new InmutableList(new Object?[] { "a", null, "c" });
        Assert.IsTrue(this.lista.Contains("a"));
        Assert.IsTrue(this.lista.Contains(null));
        Assert.IsFalse(this.lista.Contains("b"));
    }

    [TestMethod]
    public void TestRemove()
    {
        this.lista = new InmutableList(new Object?[] { "a", null, "b", "a" });
        var nuevaLista = this.lista.Remove("a");
        Assert.AreEqual(2, nuevaLista.Count);
        Assert.IsTrue(nuevaLista.Contains(null));
        Assert.IsTrue(nuevaLista.Contains("b"));
        Assert.IsFalse(nuevaLista.Contains("a"));
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var nuevaLista = this.lista.RemoveAt(1);
        Assert.AreEqual(2, nuevaLista.Count);
        Assert.AreEqual("a", nuevaLista.ElementAt(0));
        Assert.AreEqual("c", nuevaLista.ElementAt(1));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void TestRemoveAt_IndexOutOfRangeException()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var nuevaLista = this.lista.RemoveAt(3); // Índice fuera de rango
    }

    [TestMethod]
    public void TestClear()
    {
        this.lista = new InmutableList(new Object?[] { "a", "b", "c" });
        var nuevaLista = this.lista.Clear();
        Assert.AreEqual(0, nuevaLista.Count);
    }

}

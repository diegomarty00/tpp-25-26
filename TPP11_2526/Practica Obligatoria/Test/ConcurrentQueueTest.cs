namespace Test;

using System;
using Listas;

[TestClass]
public class ConcurrentQueueTests
{
    // ---------- ESTADO INICIAL ----------

    [TestMethod]
    public void NuevaCola_EstaVacia()
    {
        var cola = new ConcurrentQueue<int>();

        Assert.AreEqual(0, cola.Count);
        Assert.IsTrue(cola.IsEmpty());
    }

    // ---------- ENQUEUE ----------

    [TestMethod]
    public void Enqueue_UnElemento_CountEsUno()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Enqueue(42);

        Assert.AreEqual(1, cola.Count);
        Assert.IsFalse(cola.IsEmpty());
    }

    [TestMethod]
    public void Enqueue_VariosElementos_CountCorrecto()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Enqueue(1);
        cola.Enqueue(2);
        cola.Enqueue(3);

        Assert.AreEqual(3, cola.Count);
    }

    // ---------- DEQUEUE ----------

    [TestMethod]
    public void Dequeue_RespetaOrdenFIFO()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Enqueue(10);
        cola.Enqueue(20);
        cola.Enqueue(30);

        Assert.AreEqual(10, cola.Dequeue());
        Assert.AreEqual(20, cola.Dequeue());
        Assert.AreEqual(30, cola.Dequeue());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Dequeue_ColaVacia_LanzaExcepcion()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Dequeue();
    }

    // ---------- TRYDEQUEUE ----------

    [TestMethod]
    public void TryDequeue_ColaVacia_DevuelveFalse()
    {
        var cola = new ConcurrentQueue<int>();

        var resultado = cola.TryDequeue(out int valor);

        Assert.IsFalse(resultado);
        Assert.AreEqual(default(int), valor);
    }

    [TestMethod]
    public void TryDequeue_ColaConElementos_DevuelveTrue()
    {
        var cola = new ConcurrentQueue<int>();
        cola.Enqueue(99);

        var resultado = cola.TryDequeue(out int valor);

        Assert.IsTrue(resultado);
        Assert.AreEqual(99, valor);
        Assert.IsTrue(cola.IsEmpty());
    }

    // ---------- PEEK ----------

    [TestMethod]
    public void Peek_NoEliminaElemento()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Enqueue(5);
        cola.Enqueue(6);

        var valor = cola.Peek();

        Assert.AreEqual(5, valor);
        Assert.AreEqual(2, cola.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Peek_ColaVacia_LanzaExcepcion()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Peek();
    }

    // ---------- TRIYPEEK ----------

    [TestMethod]
    public void TryPeek_ColaVacia_DevuelveFalse()
    {
        var cola = new ConcurrentQueue<int>();

        var resultado = cola.TryPeek(out int valor);

        Assert.IsFalse(resultado);
        Assert.AreEqual(default(int), valor);
    }

    [TestMethod]
    public void TryPeek_ColaConDatos_DevuelveTrueSinEliminar()
    {
        var cola = new ConcurrentQueue<int>();
        cola.Enqueue(7);

        var resultado = cola.TryPeek(out int valor);

        Assert.IsTrue(resultado);
        Assert.AreEqual(7, valor);
        Assert.AreEqual(1, cola.Count);
    }

    // ---------- SECUENCIA COMPLETA ----------

    [TestMethod]
    public void SecuenciaCompleta_EnqueueDequeue_FinalmenteVacia()
    {
        var cola = new ConcurrentQueue<int>();

        cola.Enqueue(1);
        cola.Enqueue(2);
        cola.Enqueue(3);

        cola.Dequeue();
        cola.Dequeue();
        cola.Dequeue();

        Assert.IsTrue(cola.IsEmpty());
        Assert.AreEqual(0, cola.Count);
    }
}



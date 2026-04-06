namespace ForLoopTest;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ForLoopTests
{
    [TestMethod]
    public void Test_ForLoop_ConteoBasico()
    {
        int i = -1;
        int suma = 0;

        Action init = () => i = 0;
        Func<bool> cond = () => i < 5;
        Action iter = () => i++;
        Action body = () => suma += i;

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual(10, suma); // 0 + 1 + 2 + 3 + 4
    }

    [TestMethod]
    public void Test_ForLoop_NoEjecutaSiCondicionInicialEsFalsa()
    {
        int i = -1;
        int contador = 0;

        Action init = () => i = 10;
        Func<bool> cond = () => i < 0; // Falso desde el inicio
        Action iter = () => i--;
        Action body = () => contador++;

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual(0, contador);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Test_ForLoop_ParametroNull_LanzaExcepcion()
    {
        ForLoop.Run(null!, () => true, () => { }, () => { });
    }

    [TestMethod]
    public void Test_ForLoop_FuncionaConStrings()
    {
        string cadena = "";
        int i = -1;

        Action init = () => i = 0;
        Func<bool> cond = () => i < 3;
        Action iter = () => i++;
        Action body = () => cadena += "x";

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual("xxx", cadena);
    }

    [TestMethod]
    public void Test_ForLoop_IteracionCompleja()
    {
        int i = -1;
        int producto = 1;

        Action init = () => i = 1;
        Func<bool> cond = () => i <= 4;
        Action iter = () => i++;
        Action body = () => producto *= i;

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual(24, producto); // 1 * 2 * 3 * 4
    }

    [TestMethod]
    public void Test_ForLoop_CondicionQueSeVuelveFalsaAlInstante()
    {
        int i = -1;
        int llamadas = 0;

        Action init = () => i = 5;
        Func<bool> cond = () => i < 5;
        Action iter = () => i++;
        Action body = () => llamadas++;

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual(0, llamadas);
    }

    [TestMethod]
    public void Test_ForLoop_IterationNuncaEjecutadaSiCondicionFallaPrimero()
    {
        int i = 0;
        int iteraciones = 0;

        Action init = () => i = 10;
        Func<bool> cond = () => false; // Nunca entra
        Action iter = () => iteraciones++;
        Action body = () => { };

        ForLoop.Run(init, cond, iter, body);

        Assert.AreEqual(0, iteraciones);
    }
}
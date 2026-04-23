
namespace Console_App;

using System.Diagnostics;
using TPL;
class Program
{
    static void Main(string[] args)
    {
        string textoOrigen = ProcesadorTextos.LeerFicheroTexto("../../../../clarin.txt");
        string texto = "";
        string aux = "";

        const int MAX_PROCESAMIENTOS = 10;

        for (int i = 0; i < MAX_PROCESAMIENTOS; i++)
        {
            aux = texto + " " + textoOrigen;
            texto = aux;
        }

        string[] palabras = ProcesadorTextos.DividirEnPalabras(texto);

        var paraleloResult = IndependientesParalelo(palabras);
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine();
        var secuencialResult = IndependientesSecuencial(palabras);

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("El porcentaje de mejora es: " + ((double)(secuencialResult - paraleloResult) / secuencialResult * 100) + " %.");
        Console.WriteLine("==============================================");
    }

    private static long IndependientesSecuencial(string[] palabras)
    {
        Stopwatch sw1 = Stopwatch.StartNew();
        var resultado = ProcesadorTextos.ContarPalabrasSecuencial(palabras);
        sw1.Stop();
        Stopwatch sw2 = Stopwatch.StartNew();
        var masRepetida = ProcesadorTextos.PalabraMasRepetidaSecuencial(resultado);
        var menosRepetida = ProcesadorTextos.PalabraMenosRepetidaSecuencial(resultado);
        sw2.Stop();

        Console.WriteLine($"[Secuencial] Palabras distintas: {resultado.Count}");
        Console.WriteLine($"[Secuencial] Tiempo Contar: {sw1.ElapsedMilliseconds} ms.");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine($"[Secuencial] Palabra más repetida: {masRepetida.Key} ({masRepetida.Value} veces)");
        Console.WriteLine($"[Secuencial] Palabra menos repetida: {menosRepetida.Key} ({menosRepetida.Value} veces)");
        Console.WriteLine($"[Secuencial] Tiempo Buscar: {sw2.ElapsedMilliseconds} ms.");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("[Secuencial] Suma de tiempos: " + (sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds) + " ms.");

        return sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds;
    }

    private static long IndependientesParalelo(string[] palabras)
    {
        Stopwatch sw1 = Stopwatch.StartNew();
        var resultado = ProcesadorTextos.ContarPalabrasParalelo(palabras);
        sw1.Stop();

        Stopwatch sw2 = Stopwatch.StartNew();
        var masRepetida = ProcesadorTextos.PalabraMasRepetidaParalelo(resultado);
        var menosRepetida = ProcesadorTextos.PalabraMenosRepetidaParalelo(resultado);
        sw2.Stop();

        Console.WriteLine($"[TPL] Palabras distintas: {resultado.Count}");
        Console.WriteLine($"[TPL] Tiempo Contar: {sw1.ElapsedMilliseconds} ms.");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine($"[TPL] Palabra más repetida: {masRepetida.Key} ({masRepetida.Value} veces)");
        Console.WriteLine($"[TPL] Palabra menos repetida: {menosRepetida.Key} ({menosRepetida.Value} veces)");
        Console.WriteLine($"[TPL] Tiempo Buscar: {sw2.ElapsedMilliseconds} ms.");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("[TPL] Suma de tiempos: " + (sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds) + " ms.");

        return sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds;
    }
}

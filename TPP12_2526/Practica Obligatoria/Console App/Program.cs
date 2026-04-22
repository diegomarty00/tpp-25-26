
namespace Console_App;

using System.Diagnostics;
using TPL;
class Program
{
    static void Main(string[] args)
    {
        string texto = ProcesadorTextos.LeerFicheroTexto("../../../../clarin.txt");
        string[] palabras = ProcesadorTextos.DividirEnPalabras(texto);

        if (args.Length > 0 && args[0] == "p")
            IndependientesParalelo(texto, palabras);
        else
           IndependientesSecuencial(texto, palabras);
    }

    private static void IndependientesSecuencial(string texto, string[] palabras)
    {
        Stopwatch sw = Stopwatch.StartNew();
        var resultado = ProcesadorTextos.ContarPalabrasSecuencial(palabras);
        sw.Stop();
        Console.WriteLine($"[Secuencial] Palabras distintas: {resultadoSec.Count}");
        Console.WriteLine($"[Secuencial] Tiempo: {sw.ElapsedMilliseconds} ms."); 
    }

    private static void IndependientesParalelo(string texto, string[] palabras)
    {
        Stopwatch sw = Stopwatch.StartNew();
        var resultado = ProcesadorTextos.ContarPalabrasParalelo(palabras);
        sw.Stop();
        Console.WriteLine($"[TPL] Palabras distintas: {resultado.Count}");
        Console.WriteLine($"[TPL] Tiempo: {sw.ElapsedMilliseconds} ms."); 
    }
}

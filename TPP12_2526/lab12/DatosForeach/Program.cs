using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DatosForeach;

class Program
{
    static void Main(string[] args)
    {
        string dirImagenes = Path.GetFullPath("../../../../pics");
        string dirDestino = Path.GetFullPath("../../../../pics/rotated");
        Directory.CreateDirectory(dirDestino);

        string[] ficheros = Directory.GetFiles(dirImagenes, "pic*.jpg");

        // [Windows] DatosForEach.exe p
        if (args.Length > 0 && args[0] == "p")
            ForEachParalelo(ficheros, dirDestino);
        else
            ForEachSecuencial(ficheros, dirDestino);
    }

    static void ForEachSecuencial(IEnumerable<string> ficheros, string dirDestino)
    {
        Stopwatch sw = Stopwatch.StartNew();
        foreach (string fichero in ficheros)
        {
            string nombreFichero = Path.GetFileName(fichero);
            Imprimir($"[ForEach secuencial] Procesando el fichero \"{nombreFichero}\" con el hilo ID={Thread.CurrentThread.ManagedThreadId}.");
            using Image image = Image.Load(fichero);
            image.Mutate(x => x.Rotate(180));
            image.Save(Path.Combine(dirDestino, nombreFichero));
        }
        sw.Stop();
        Console.WriteLine($"[ForEach secuencial] Tiempo: {sw.ElapsedMilliseconds} ms.");
    }

    // Task Parallel Library (TPL)
    // Escala dinámicamente el número de hilos creados en función del número de CPUs o cores
    // Escala y gestiona dinámicamente el número de hilos creados en función del Thread Pool
    // Uso del ForEach
    // Cuidado con los globales y por los posibles conflictos con bloqueos

    static void ForEachParalelo(IEnumerable<string> ficheros, string dirDestino)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Parallel.ForEach(ficheros, (fichero) =>
        {
            string nombreFichero = Path.GetFileName(fichero);
            Imprimir($"[ForEach TPL] Procesando el fichero \"{nombreFichero}\" con el hilo ID={Thread.CurrentThread.ManagedThreadId}.");
            using Image image = Image.Load(fichero);
            image.Mutate(x => x.Rotate(180));
            image.Save(Path.Combine(dirDestino, nombreFichero));            
        });
        sw.Stop();
        Console.WriteLine($"[ForEach TPL] Tiempo: {sw.ElapsedMilliseconds} ms.");        
    }

    [Conditional("DEBUG")]
    static void Imprimir(string texto)
    {
        Console.WriteLine(texto);
    }



}

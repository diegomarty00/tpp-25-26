namespace TPL;

using System;
using System.Text;
using System.Collections.Concurrent;

public static class ProcesadorTextos
{

    /// <summary>
    /// Lee un fichero
    /// </summary>
    /// <param name="fileName">Nombre del fichero de entrada</param>
    /// <returns>String con el texto del fichero</returns>
    public static String LeerFicheroTexto(string nombreFichero)
    {
        using (StreamReader stream = File.OpenText(nombreFichero))
        {
            StringBuilder text = new StringBuilder();
            string? linea;
            while ((linea = stream.ReadLine()) != null)
            {
                text.AppendLine(linea);
            }
            return text.ToString();
        }
    }

    /// <summary>
    /// Divide un texto en palabras
    /// </summary>
    public static string[] DividirEnPalabras(String texto)
    {
        return texto.Split(new char[] { ' ', '\r', '\n', ',', '.', ';', ':', '-', '!', '¡', '¿', '?', '/', '«',
                                        '»', '_', '(', ')', '\"', '*', '\'', 'º', '[', ']', '#' },
            StringSplitOptions.RemoveEmptyEntries);
    }



    public static Dictionary<string, int> ContarPalabrasSecuencial(string[] palabras)
    {
        Dictionary<string, int> contador = new();

        foreach (var palabra in palabras)
        {
            string p = palabra.ToLower();
            if (contador.ContainsKey(p))
                contador[p]++;
            else
                contador[p] = 1;
        }

        return contador;
    }

    public static KeyValuePair<string, int> PalabraMasRepetidaSecuencial(
        Dictionary<string, int> contador)
    {
        return contador
            .OrderByDescending(p => p.Value)
            .First();
    }

    public static KeyValuePair<string, int> PalabraMenosRepetidaSecuencial(
        Dictionary<string, int> contador)
    {
        return contador
            .OrderBy(p => p.Value)
            .First();
    }

    public static Dictionary<string, int> ContarPalabrasParalelo(string[] palabras)
    {
        ConcurrentDictionary<string, int> contador = new();

        Parallel.ForEach(palabras, palabra =>
        {
            string p = palabra.ToLower();
            contador.AddOrUpdate(p, 1, (_, valorActual) => valorActual + 1);
        });

        return new Dictionary<string, int>(contador);
    }

    public static KeyValuePair<string, int> PalabraMasRepetidaParalelo(
    Dictionary<string, int> contador)
    {
        return contador.Aggregate((a, b) => a.Value > b.Value ? a : b);
    }

    public static KeyValuePair<string, int> PalabraMenosRepetidaParalelo(
        Dictionary<string, int> contador)
    {
        return contador.Aggregate((a, b) => a.Value < b.Value ? a : b);
    }


}

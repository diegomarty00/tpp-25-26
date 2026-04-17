namespace TPL;

using System;
using System.Text;

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

}

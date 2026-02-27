namespace lab06;

class Program
{
    static void Main()
    {

        // No currificada vs currificada
        bool res1 = StartsWith("pe", "perezoso");
        Console.WriteLine($"\"StartsWith(pe, perezoso)\": { res1 }");

        bool res2 = CurriedStartsWith("pe")("perezoso");
        Console.WriteLine($"CurriedStartsWith(\"pe\")(\"perezoso\"): { res2 }");

        // ¿Qué estámos usando aquí?
        var EsUrlSegura = CurriedStartsWith("https://");
        Console.WriteLine($"¿Es segura https://uniovi.es ? {EsUrlSegura("https://uniovi.es")}");
        Console.WriteLine($"¿Es segura http://uniovi.es ? {EsUrlSegura("http://uniovi.es")}");

        Console.WriteLine($"EstaEnRango(0, 10, 5): {EstaEnRango(0, 10, 5)}");
        // Implementa la versión currificada de EstaEnRango
        // Empleando la anterior, crea "EstaEnEdadLaboral" [16, 67]
        // Currifica la función Suma. En cada devolución, la función solamente recibirá un único valor


    }

    static bool StartsWith(string prefijo, string texto)
    {
        return texto.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase);
    }

    static Func<string, bool> CurriedStartsWith(string prefijo)
    {
        return texto => texto.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase);
    }

    static bool EstaEnRango(int min, int max, int x)
    {
        return x >= min && x <= max;
    }
}

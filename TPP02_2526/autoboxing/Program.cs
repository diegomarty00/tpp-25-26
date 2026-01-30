namespace autoboxing;

/// <summary>
/// Boxing, Unboxing. Operadores "is" y "as".
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        //Value types
        int i1 = 42;
        Int32 i2 = i1;
        Console.WriteLine($"Int32 vs int: {typeof(Int32) == typeof(int)}");  


        //Boxing
        object o1 = i1;
        object o2 = (object)i1;
        //¿Qué podemos afirmar sobre o1 y o2?

        //Unboxing
        int i3 = (int)o1;
        // long mal = (long)o1; // InvalidCastException: Int32 vs Int64

                   
        object o3 = Boxing(100);
        int i4 = Unboxing(o3);


        //Operador is
        object s1 = "Hello, World!";
        if (s1 is string)
        {
            Console.WriteLine("Longitud: {0}.", ((string)s1).Length); //OJO
        }


        //Operador as
        string? s2 = s1 as string;
        if (s2 != null)
        {
            Console.WriteLine("Longitud: {0}.", s2.Length);
        }

        //¿Cuándo usamos is y cuándo usamos as?

        /*
            EJERCICIO: Implementa un método estático ContarStrings(object[] items) 
            que reciba un array de object con elementos de distintos tipos (int, string, double)
            y devuelva cuántos de esos elementos son string.
        */
    }

    static int Unboxing(object o)
    {
        return (int)o;
    }

    static object Boxing(int i)
    {
        return i;
    }
    
}

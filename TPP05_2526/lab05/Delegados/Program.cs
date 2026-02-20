namespace Delegados;

class Program
{

    // Definimos un tipo delegado (delegate) que representa cualquier método compatible
    // con la firma: recibe 1 int y devuelve un int.
    // Lo tipos delegados son nominales.
    // ¿Qué posibilitan los tipos delegados?
    public delegate int Function(int n1);

    public delegate R GenericFunction<T, R>(T a);
    public delegate R GenericFunction<T1, T2, R>(T1 a, T2 b);

    //¿Cómo se definiría el delegado para el método Concatenar, Multiplica y Media?


    static void Main()
    {
        // El método Cuadrado es compatible con el delegado Function
        Function cuadrado = Cuadrado;
        Console.WriteLine(cuadrado(5));

        // Func admite de 0 a 16 parámetros de entrada y siempre devuelve un valor (TResult).
        Func<int, int> cuadradoFunc = Cuadrado;
        Console.WriteLine(cuadradoFunc(5));

        Func<string, string, string> concatenarFunc = Concatenar;
        Console.WriteLine(concatenarFunc("Hola ", "Mundo"));

        Func<int, int, int> multiplicarFunc = Multiplicar;
        Console.WriteLine(multiplicarFunc(3, 4));

        Func<int, int, double> mediaFunc = Media;
        Console.WriteLine(mediaFunc(10, 20));


        // Predicate admite único parámetro y siempre devuelve bool.
        Predicate<int> esParPredicate = EsPar;
        Console.WriteLine(esParPredicate(cuadrado(5))); // false
        Console.WriteLine(esParPredicate(cuadrado(4))); // true

        // Action admite de 0 a 16 parámetros de entrada y el retorno es void.
        Action<string> imprimirAction = Imprimir;
        imprimirAction(concatenarFunc("Hola ", "Mundo"));

    }
    static int Cuadrado(int a)
    {
        return a * a;
    }

    static string Concatenar(string a, string b)
    {
        return a + b;
    }

    static int Multiplicar(int n1, int n2)
    {
        return n1 * n2;
    }

    static double Media(int a, int b)
    {
        return (a + b) / 2.0;
    }

    static void Imprimir(string mensaje)
    {
        Console.WriteLine(mensaje);
    }

    static bool EsPar(int n)
    {
        return n % 2 == 0;
    }

}

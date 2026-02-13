namespace Genericidad;

using Temperaturas;


/// <summary>
/// 
/// </summary>
class Program
{

    // El valor (object) de la Medida con la Fecha más reciente.
    static object MasReciente(Medida a, Medida b)
    {
        return a.Fecha > b.Fecha ? a.Valor : b.Valor;
    }

    /* Implementa una versión del anterior apoyada en genericidad. */
    static T MasRecienteGen<T>(MedidaGen<T> a, MedidaGen<T> b)
    {
        return a.Fecha > b.Fecha ? a.Valor : b.Valor;
    }
    

    static void Main()
    {
        var celsius1 = new Celsius(25);
        var fahren1 = new Fahrenheit(80.6);
        var fecha1 = new DateTime(2024, 1, 1, 10, 0, 0);
        var fecha2 = new DateTime(2024, 1, 1, 12, 0, 0);     
        var medida1 = new Medida { Valor = celsius1, Fecha = fecha1 };
        var medida2 = new Medida { Valor = fahren1, Fecha = fecha2 };

        var medidaGen1 = new MedidaGen<Celsius> { Valor = celsius1, Fecha = fecha1 };
        var medidaGen2 = new MedidaGen<Fahrenheit> { Valor = fahren1, Fecha = fecha2 };

        //var medidaRecienteGen = MasRecienteGen(medidaGen1, medidaGen2);

        object medidaReciente = MasReciente(medida1, medida2);
        Console.WriteLine($"Medida más reciente: {medidaReciente}");       
 
        //EjemploMaxV1();
        //EjemploMaxV2();

        EjemploMaxV4();


    }


    static IComparable MaxV1(IComparable a, IComparable b)
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    static void EjemploMaxV1()
    {
        var c = new Celsius(25);
        var f = new Fahrenheit(77); //25

        Celsius resultado11 = (Celsius) MaxV1(c, f);
        Console.WriteLine($"MaxV1(c, f) -> (Celsius) = {resultado11}"); //PETA

        Fahrenheit resultado12 = (Fahrenheit) MaxV1(c, f);
        Console.WriteLine($"MaxV1(c, f) -> (Fahrenheit) = {resultado12}");

        Celsius resultado13 = (Celsius) MaxV1(f, c);
        Console.WriteLine($"MaxV1(f, c) -> (Celsius) = {resultado13}");

        Fahrenheit resultado14 = (Fahrenheit) MaxV1(f, c);
        Console.WriteLine($"MaxV1(f, c) -> (Fahrenheit) = {resultado14}");  //PETA 

    }



    static IComparable<T> MaxV2<T>(IComparable<T> a, IComparable<T> b)
    {
         return a.CompareTo((T)b) > 0 ? a : b; //Rompe cuando el T no puede castear al b
    }

  

    // Empiezan las comparaciones cruzadas.
    static void EjemploMaxV2()
    {
        var c = new Celsius(25);
        var f = new Fahrenheit(77);

        IComparable<Celsius> resultado21 = MaxV2<Celsius>(c, f);
        Console.WriteLine($"MaxV2<Celsius>(c, f) -> IComparable<Celsius> = {resultado21}"); //PETA 

        IComparable<Fahrenheit> resultado22 = MaxV2<Fahrenheit>(c, f);
        Console.WriteLine($"MaxV2<Fahrenheit>(c, f) -> IComparable<Fahrenheit> = {resultado22}");

        IComparable<Celsius> resultado23 = MaxV2<Celsius>(f, c);
        Console.WriteLine($"MaxV2<Celsius>(f, c) -> IComparable<Celsius> = {resultado23}");

        IComparable<Fahrenheit> resultado24 = MaxV2<Fahrenheit>(f, c);
        Console.WriteLine($"MaxV2<Fahrenheit>(f, c) -> IComparable<Fahrenheit> = {resultado24}"); //PETA 
    }

    /* Existiría una tercera (mala) opción: Se basaría en que Max devuelva directamente T. */



    /* MaxV4 hace uso de la genericidad acotada.*/
    /* https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters */


    static T MaxV4<T>(T a, T b) where T : IComparable
    {
         return a.CompareTo(b) > 0 ? a : b;
    }

    static void EjemploMaxV4()
    {
        var c = new Celsius(25);
        var f = new Fahrenheit(77);

        var resultado1 = MaxV4<IComparable>(c, f);
        Console.WriteLine($"MaxV4<Celsius>(c, f) -> IComparable<Celsius> = {resultado1}");


        var resultado2 = MaxV4<IComparable>(f, c);
        Console.WriteLine($"MaxV4<Celsius>(f, c) -> IComparable<Celsius> = {resultado2}");
        
    }


    /*
        Implementa un tipo genérico MyNullable<T> que represente la ausencia o presencia de un valor de tipo T.
        Requisitos:
        * La clase debe exponer:
            - HasValue: indica si hay valor almacenado.
            - Value: devuelve el valor almacenado. Si no hay valor, lanza InvalidOperationException.
            - GetValueOrDefault: devuelve Value si HasValue es true. El valor por defecto de T si HashValue es false.

        Comprueba que funciona correctamente para int.
        
        * Añade el código necesario para que no puedan crearse MyNullables con tipos de referencia (object, string,...).


    */

}

internal interface ITemperature
{
}
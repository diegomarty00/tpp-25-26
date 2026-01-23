namespace Console_App;
using System;
using Library;

class Program
{
    static void Main(string[] args)
    {
        Cliente c = new Cliente { Nombre = "Alice", Edad = 30 };
        Console.WriteLine(c.ToString());

        Order o = new Order {   Id = "ORD123", 
                                NombreCliente = c.Nombre,
                                NumeroArticulos = 5, 
                                PrecioUnitario = 20.00m, 
                                GastosEnvio = 5.00m };

        Console.WriteLine(o.ToString());
    }
}

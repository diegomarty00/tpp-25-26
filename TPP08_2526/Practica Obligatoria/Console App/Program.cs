namespace Console_App;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maybe;


public interface Identificable
{
    string Id { get; }
}

public record Vendedor(string Id, string Nombre, string Email) : Identificable;
public record Producto(string Id, string Descripcion, uint Stock) : Identificable;
public record Venta(string VendedorId, string ProductoId, uint Cantidad);


public static class Program
{
    static void Main()
    {
        var vendedores = CargarDiccionario<Vendedor>(
            "../../../../data/sellers.csv",
            CargarVendedor
        );

        var productos = CargarDiccionario<Producto>(
            "../../../../data/products.csv",
            CargarProducto
        );

        var ventas = CargarLista<Venta>(
            "../../../../data/sales.csv",
            ConfigCargarVenta(
                vendedores.OrElse(new Dictionary<string, Vendedor>()),
                productos.OrElse(new Dictionary<string, Producto>())
            )
        );

        Console.WriteLine("Vendedores:");
        vendedores.Match(
            some =>
            {
                foreach (var v in some.Values)
                    Console.WriteLine($"- {v.Id} {v.Nombre} {v.Email}");
            },
            () => Console.WriteLine("Error al cargar vendedores")
        );

        Console.WriteLine("\nProductos:");
        productos.Match(
            some =>
            {
                foreach (var p in some.Values)
                    Console.WriteLine($"- {p.Id} {p.Descripcion} Stock:{p.Stock}");
            },
            () => Console.WriteLine("Error al cargar productos")
        );

        Console.WriteLine("\nVentas:");
        ventas.Match(
            some =>
            {
                foreach (var v in some)
                    Console.WriteLine($"- {v.VendedorId} {v.ProductoId} x{v.Cantidad}");
            },
            () => Console.WriteLine("Error al cargar ventas")
        );
    }


    private static Maybe<uint> ParseUint(string entrada)
    {
        try
        {
            return new Some<uint>(uint.Parse(entrada));
        }
        catch
        {
            return new None<uint>();
        }
    }

    private static Maybe<Vendedor> CargarVendedor(int num, List<string> campos)
    {
        if (campos.Count != 3)
            return new None<Vendedor>();

        return new Some<Vendedor>(
            new Vendedor(campos[0], campos[1], campos[2])
        );
    }

    private static Maybe<Producto> CargarProducto(int num, List<string> campos)
    {
        if (campos.Count != 3)
            return new None<Producto>();

        return ParseUint(campos[2]).Match<Maybe<Producto>>(
            ok => new Some<Producto>(
                new Producto(campos[0], campos[1], ok)
            ),
            () => new None<Producto>()
        );
    }

    private static Func<int, List<string>, Maybe<Venta>>
        ConfigCargarVenta(Dictionary<string, Vendedor> vendedores,
                          Dictionary<string, Producto> productos)
    {
        return (num, campos) =>
        {
            if (campos.Count != 3)
                return new None<Venta>();

            string vendedorId = campos[0];
            string productoId = campos[1];

            if (!vendedores.ContainsKey(vendedorId))
                return new None<Venta>();

            if (!productos.ContainsKey(productoId))
                return new None<Venta>();

            return ParseUint(campos[2]).Match<Maybe<Venta>>(
                ok => new Some<Venta>(
                    new Venta(vendedorId, productoId, ok)
                ),
                () => new None<Venta>()
            );
        };
    }

    private static Maybe<Dictionary<string, T>>
        CargarDiccionario<T>(string ruta, Func<int, List<string>, Maybe<T>> f)
        where T : Identificable
    {
        var dic = new Dictionary<string, T>();

        foreach (var resultado in CargarFilas(ruta, f))
        {
            if (resultado is None<T>)
                return new None<Dictionary<string, T>>();

            resultado.Match(
                some =>
                {
                    if (!dic.ContainsKey(some.Id))
                        dic[some.Id] = some;
                },
                () => { }
            );
        }

        return new Some<Dictionary<string, T>>(dic);
    }

    private static Maybe<List<T>>
        CargarLista<T>(string ruta, Func<int, List<string>, Maybe<T>> f)
    {
        var lista = new List<T>();

        foreach (var resultado in CargarFilas(ruta, f))
        {
            if (resultado is None<T>)
                return new None<List<T>>();

            resultado.Match(
                some => lista.Add(some),
                () => { }
            );
        }

        return new Some<List<T>>(lista);
    }

    private static IEnumerable<Maybe<T>>
        CargarFilas<T>(string ruta, Func<int, List<string>, Maybe<T>> f)
    {
        return File.ReadLines(ruta)
            .Zip(Enumerable.Range(1, int.MaxValue))
            .Map(t => (num: t.Item2, linea: t.Item1))
            .Filter(t => !string.IsNullOrWhiteSpace(t.linea))
            .Filter(t => !t.linea.Trim().StartsWith('#'))
            .Map(t => (t.num, campos: t.linea.Split(';').Map(s => s.Trim()).ToList()))
            .Map(t => f(t.num, t.campos));
    }


    public static IEnumerable<T2> Map<T1, T2>(this IEnumerable<T1> xs, Func<T1, T2> f)
    {
        foreach (var x in xs)
            yield return f(x);
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> xs, Predicate<T> p)
    {
        foreach (var x in xs)
            if (p(x))
                yield return x;
    }

    public static T2 Reduce<T1, T2>(this IEnumerable<T1> xs, T2 seed, Func<T2, T1, T2> f)
    {
        var acc = seed;
        foreach (var x in xs)
            acc = f(acc, x);
        return acc;
    }

    public static IEnumerable<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> xs, IEnumerable<T2> ys)
    {
        using var e1 = xs.GetEnumerator();
        using var e2 = ys.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext())
            yield return (e1.Current, e2.Current);
    }
}


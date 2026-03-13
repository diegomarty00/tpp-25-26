using System.Runtime.CompilerServices;

namespace tpp.lab08;

public interface Identificable
{
    string Id { get; }
}

public record Vendedor(string Id, string Nombre, string Email) : Identificable;
public record Producto(string Id, string Descripcion, uint Stock) : Identificable;
public record Venta(string VendedorId, string ProductoId, uint Cantidad);

// Convertir el proyecto de VentasExcepciones
// para seguir un enfoque basado en Result
// Orden recomendado:
// ParseUint: Aquí no queda otra que capturar las excepciones para que no se propaguen más.
// Cargar vendedor, producto y la configuración de la venta.
// CargarLista y Cargar Diccionario: Acumula éxitos y errores.

static class Program
{
    static void Main()
    {
        //TODO: llamar a CargarDiccionario con "../../../../data/sellers.bad.csv"
        //TODO: llamar a CargarDiccionario con "../../../../data/products.bad.csv"
        //TODO: llamar a CargarLista con "../../../../data/sales.bad.csv"

        // TODO: Cuando termines de implementar, acuérdate de probar también con los CSV buenos:
        // - ../../../../data/sellers.csv
        // - ../../../../data/products.csv
        // - ../../../../data/sales.csv


        var vendedores = CargarDiccionario<Vendedor>("../../../../data/sellers.bad.csv", CargarVendedor);
        var productos = CargarDiccionario<Producto>("../../../../data/products.bad.csv", CargarProducto);
        var ventas = CargarLista<Venta>("../../../../data/sales.bad.csv", 
            ConfigCargarVenta(
                vendedores.OrElse(new Dictionary<string, Vendedor>()), 
                productos.OrElse(new Dictionary<string, Producto>())));
            


        Console.WriteLine("Vendedores:");
        vendedores.Match(
            success =>
            {
                foreach (var vendedor in success.Values)
                    Console.WriteLine($"- ID: {vendedor.Id}, Nombre: {vendedor.Nombre}, Email: {vendedor.Email}");
            },
            error =>
            {
                Console.WriteLine("Errores al cargar vendedores:");
                foreach (var err in error)
                    Console.WriteLine(err);
            });

        Console.WriteLine("\nProductos:");
        productos.Match(
            success =>
            {
                foreach (var producto in success.Values)
                    Console.WriteLine($"- ID: {producto.Id}, Descripción: {producto.Descripcion}, Stock: {producto.Stock}");
            },
            error =>
            {
                Console.WriteLine("Errores al cargar productos:");
                foreach (var err in error)
                    Console.WriteLine(err);
            });
        Console.WriteLine("\nVentas:");
        ventas.Match(
            success =>
            {
                foreach (var venta in success)
                    Console.WriteLine($"- Vendedor: {venta.VendedorId}, Producto: {venta.ProductoId}, Cantidad: {venta.Cantidad}");
            },
            error =>
            {
                Console.WriteLine("Errores al cargar ventas:");
                foreach (var err in error)
                    Console.WriteLine(err);
            });
    }


    private static Result<Vendedor, string> CargarVendedor(int num, List<string> campos)
    {
        if (campos.Count != 3)
            return new Failure<Vendedor, string>($"Línea {num}: Se esperaban 3 campos, pero se encontraron {campos.Count}.");

        return new Success<Vendedor, string>(
                new Vendedor(Id: campos[0], Nombre: campos[1], Email: campos[2])
                );
    }

    private static Result<Producto, string> CargarProducto(int num, List<string> campos)
    {
        
        if (campos.Count != 3)
            return new Failure<Producto, string>($"Línea {num}: Se esperaban 3 campos, pero se encontraron {campos.Count}.");

        return ParseUint(campos[2]).Match<Result<Producto, string>>(
            success => new Success<Producto, string>(
                new Producto(Id: campos[0], Descripcion: campos[1], Stock: success)
                ),
            error => new Failure<Producto, string>(
                $"Línea {num}: El campo de stock no es un número entero válido. Detalles: {error}"
                )
        );
    }

    private static Func<int, List<string>, Result<Venta, string>> ConfigCargarVenta(Dictionary<string, Vendedor> vendedores, Dictionary<string, Producto> productos)
    {
        return (num, campos) =>
        {
            if (campos.Count != 3)
                return new Failure<Venta, string>($"Línea {num}: Se esperaban 3 campos, pero se encontraron {campos.Count}.");

            string vendedorId = campos[0];
            string productoId = campos[1];

            if (!vendedores.ContainsKey(vendedorId))
                return new Failure<Venta, string>($"Línea {num}: El vendedor con ID '{vendedorId}' no existe.");

            if (!productos.ContainsKey(productoId))
                return new Failure<Venta, string>($"Línea {num}: El producto con ID '{productoId}' no existe.");

            return ParseUint(campos[2]).Match<Result<Venta, string>>(
                success => new Success<Venta, string>(
                    new Venta(VendedorId: vendedorId, ProductoId: productoId, Cantidad: success)
                    ),
                error => new Failure<Venta, string>(
                    $"Línea {num}: El campo de cantidad no es un número entero válido. Detalles: {error}"
                    )
            );
        };
    }

    private static Result<uint, string> ParseUint(string entrada)
    {
        try
        {
            return new Success<uint, string>(uint.Parse(entrada));
        }
        catch (FormatException)
        {
            return new Failure<uint, string>($"'{entrada}' no es un número entero válido.");
        }
    }

    private static Result<Dictionary<string, T>, string[]> CargarDiccionario<T>(string ruta, Func<int, List<string>, Result<T, string>> f) where T : Identificable
    {
        var (diccionario, errores) = CargarFilas(ruta, f).Reduce((new Dictionary<string, T>(), new List<string>()), (acc, res) =>
        {
            res.Match(
                success =>
                {
                    if (acc.Item1.ContainsKey(success.Id))
                        acc.Item2.Add($"Línea {success.Id}: ID duplicada '{success.Id}'.");
                    else
                        acc.Item1[success.Id] = success;
                },
                error =>
                {
                    acc.Item2.Add(error);
                });

            return acc;
        });

        if (errores.Count > 0)
            return new Failure<Dictionary<string, T>, string[]>(errores.ToArray());

        return new Success<Dictionary<string, T>, string[]>(diccionario);
    }

    private static Result<List<T>, string[]> CargarLista<T>(string ruta, Func<int, List<string>, Result<T, string>> f)
    {
        var (lista, errores) = CargarFilas(ruta, f).Reduce((new List<T>(), new List<string>()), (acc, res) =>
        {
            res.Match(
                success =>
                {
                    acc.Item1.Add(success);
                },
                error =>
                {
                    acc.Item2.Add(error);
                });

            return acc;
        });

        if (errores.Count > 0)
            return new Failure<List<T>, string[]>(errores.ToArray());

        return new Success<List<T>, string[]>(lista);
    }

    private static IEnumerable<Result<T, string>> CargarFilas<T>(string ruta, Func<int, List<string>, Result<T, string>> f)
    {
        // Fíjate que esto no cambia respecto a la versión con excepciones, porque el manejo de errores lo delegamos a las funciones f.
        // Lo unco que cambia es que ahora f devuelve un Result, y por tanto el tipo de retorno de LoadRows es IEnumerable<Result<T, string>> en lugar de IEnumerable<T>.
        return File.ReadLines(ruta)
            .Zip(Enumerable.Range(1, int.MaxValue)) // Obtenemos la línea y el número de línea.
            .Map(t => (num: t.Item2, linea: t.Item1))
            .Filter(t => !string.IsNullOrWhiteSpace(t.linea)) // Filtramos líneas vacías.
            .Filter(t => !t.linea.Trim().StartsWith('#')) // Filtramos líneas que empiecen por '#'
            .Map(t => (num: t.num, campos: new List<string>(t.linea.Split(';').Map(s => s.Trim()))))
            .Map(t => f(t.num, t.campos)); // f convierte cada fila preprocesada en un Result.
    }

   // Funciones de orden superior
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

    public static T2 Reduce<T1, T2>(this IEnumerable<T1> xs, T2 semilla, Func<T2, T1, T2> f)
    {
        var acc = semilla;
        foreach (var x in xs)
            acc = f(acc, x);
        return acc;
    }

    public static IEnumerable<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> xs, IEnumerable<T2> ys)
    {
        using (var enumX = xs.GetEnumerator())
        using (var enumY = ys.GetEnumerator())
        {
            while (enumX.MoveNext() && enumY.MoveNext())
                yield return (enumX.Current, enumY.Current);
        }
    }
}



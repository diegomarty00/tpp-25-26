namespace Transacciones;

/// <summary>
/// Funciones de orden superior: Map, Filter, Reduce y Zip.
/// class vs record y uso de with
/// </summary>
public class Program
{


    /// <summary>
    /// Map es una función de orden superior que transforma uno a uno los elementos de una secuencia
    /// </summary>
    /// <typeparam name="T1">Tipo de la secuencia de entrada</typeparam>
    /// <typeparam name="T2">Tipo de la secuencia de salida</typeparam>
    /// <param name="secuencia">Secuencia de entrada</param>
    /// <param name="funcion">La función que transforma un elemento de T1 en un elemento de T2</param>
    /// <returns></returns>
    public static IEnumerable<T2> Map<T1, T2>(IEnumerable<T1> secuencia, Func<T1, T2> funcion)
    {
        IList<T2> secuenciaResultante = new List<T2>();

        foreach (T1 elemento in secuencia)
        {
            T2 transformado = funcion(elemento);
            secuenciaResultante.Add(transformado);
        }
        return secuenciaResultante;
    }

    public static IEnumerable<T> Filter<T>(IEnumerable<T> secuencia, Predicate<T> funcion)
    {
        IList<T> secuenciaResultante = new List<T>();

        foreach (T elemento in secuencia)
        {
            if (funcion(elemento))
                secuenciaResultante.Add(elemento);
        }
        return secuenciaResultante;
    }

    public static R? Reduce<T, R>(IEnumerable<T> secuencia, Func<T, R?, R?> funcion, R? acumulador = default(R))
    {

        foreach (T elemento in secuencia)
        {
            acumulador = funcion(elemento, acumulador);
        }

        return acumulador;
    }


    public static void Main()
    {

        //EjemplosImperativos();
        EjercicioTransacciones();
        EjercicioZip();
    }

    private static double Mitad(int v)
    {
        return v / 2.0;
    }

    public static void EjemplosImperativos()
    {
        int[] valores = { 1, 2, 3, 4, 5, 6 };

        // Ejemplo 1.

        IList<double> resultante = new List<double>();
        // Iteración del origen
        foreach (var v in valores)
        {
            // Operación de transformación de cada elemento
            double mitad = v / 2.0;

            // Almacenamiento o emisión del elemento resultante
            resultante.Add(mitad);
        }

        var resultMap = Map(valores, v => v / 2.0);
        var resultMapMalo = Map(valores, Mitad);

        Console.WriteLine("La mitad de cada elemento:");
        foreach (var v in resultante)
            Console.WriteLine(v);

        // Ejemplo 2.

        IList<double> resultante2 = new List<double>();
        foreach (var v in valores)
        {
            if (v % 2 == 0)
                resultante2.Add(v);
        }

        var resultFilter = Filter(valores, v => v % 2 == 0);

        Console.WriteLine("Los pares:");
        foreach (var v in resultante2)
            Console.WriteLine(v);


        // Ejemplo 3.

        int suma = 0;
        foreach (var v in valores)
            suma += v;

        var resultReduce = Reduce<int, int>(valores, (v, acc) => v + acc);
        Console.WriteLine($"La suma: {suma}");
    }


    public static void EjercicioTransacciones()
    {
        var historicoVentas = new List<Venta>
            {
                new Venta { Region = "Europa",      Estado = Estado.Cancelada, Cantidad = 100m },
                new Venta { Region = "Asia", Estado = Estado.Confirmada,  Cantidad = 500m },
                new Venta { Region = "Europa",      Estado = Estado.Cancelada, Cantidad = 200m },
                new Venta { Region = "Asia", Estado = Estado.Cancelada, Cantidad = 300m },
                new Venta { Region = "Europa",      Estado = Estado.Cancelada, Cantidad = 50m },
                new Venta { Region = "Asia", Estado = Estado.Cancelada, Cantidad = 150m },
                new Venta { Region = "Europa",      Estado = Estado.Confirmada,  Cantidad = 120m },
                new Venta { Region = "Asia", Estado = Estado.Cancelada, Cantidad = 800m },
            };

        // Cálculo del beneficio neto en Europa

        // EJERCICIO: Parte a transformar 1.
        decimal totalBeneficioEuropa = 0;
        foreach (var v in historicoVentas)
        {
            if (v.Region.ToLower() == "europa")
            {
                if (v.Estado == Estado.Confirmada)
                {
                    decimal beneficioNeto = v.Cantidad * 0.80m;
                    totalBeneficioEuropa += beneficioNeto;
                }
            }
        }

        var filterBeneficio = Filter(historicoVentas, v => v.Region.ToLower() == "europa" && v.Estado == Estado.Confirmada);
        var resultReduceBeneficio = Reduce(filterBeneficio, (v, acc) => acc + v.Cantidad * 0.80m, 0m);

        Console.WriteLine($"Beneficio neto en Europa: {resultReduceBeneficio} (Reduce)");
        Console.WriteLine($"Beneficio neto en Europa: {totalBeneficioEuropa} (Imperativo)");


        // Cálculo del beneficio medio.
        decimal total = 0;
        uint recuento = 0;
        foreach (var s in historicoVentas)
        {
            if (s.Estado == Estado.Cancelada)
            {
                continue;
            }
            decimal margen = 0;
            switch (s.Region.ToLower())
            {
                case "europa":
                    margen = 0.80m;
                    break;
                case "asia":
                    margen = 0.60m;
                    break;
                default:
                    throw new Exception("Region desconocida");
            }
            total += s.Cantidad * margen;
            recuento++;
        }
        decimal mediaBeneficio = total / recuento;

        var filterMedia = Filter(historicoVentas, v => v.Estado == Estado.Confirmada);
        var filterEuropa = Filter(filterMedia, v => v.Region.ToLower() == "europa");
        var filterAsia = Filter(filterMedia, v => v.Region.ToLower() == "asia");

        var resultReduceAsia = Reduce(filterAsia, (v, acc) => acc + v.Cantidad * 0.60m, 0m);
        var resultReduceEuropa = Reduce(filterEuropa, (v, acc) => acc + v.Cantidad * 0.80m, 0m);

        var resultReduceMedia = (resultReduceAsia + resultReduceEuropa) / (filterMedia.Count());

        Console.WriteLine($"Beneficio medio: {resultReduceMedia} (Reduce)");
        Console.WriteLine($"Beneficio medio: {mediaBeneficio} (Imperativo)");
    }


    /// <summary>
    /// Implementa una función de orden superior Zip que reciba dos secuencias IEnumerable<T> y una función Func<T1, T2, TResult>. 
    /// La función debe recorrer ambas secuencias en paralelo y devolver una nueva secuencia con el resultado de aplicar
    /// la función a cada par de elementos (uno de cada secuencia -> Tupla). 
    /// La iteración termina cuando se agote cualquiera de las dos secuencias.
    /// </summary>
    static void EjercicioZip()
    {
        //Imprímase por pantalla las tuplas resultantes de aplicar Zip a estas dos secuencias.
        var regiones = new List<string> { "Europa", "África", "Asia" };
        var margenes = new List<decimal> { 0.80m, 0.60m, 0.70m };

        var zipResult = Zip(regiones, margenes, (r, m) => (Region: r, Margen: m));

        Console.WriteLine("Tuplas resultantes de aplicar Zip:");
        foreach (var tupla in zipResult)
        {
            Console.WriteLine($"Región: {tupla.Region}, Margen: {tupla.Margen}");
        }

    }

    public static IEnumerable<TResult> Zip<T1, T2, TResult>(IEnumerable<T1> secuencia1, IEnumerable<T2> secuencia2, Func<T1, T2, TResult> funcion)
    {
        IList<TResult> secuenciaResultante = new List<TResult>();

        using (var enumerador1 = secuencia1.GetEnumerator())
        using (var enumerador2 = secuencia2.GetEnumerator())
        {
            while (enumerador1.MoveNext() && enumerador2.MoveNext())
            {
                TResult resultado = funcion(enumerador1.Current, enumerador2.Current);
                secuenciaResultante.Add(resultado);
            }
        }

        return secuenciaResultante;
    }

    // ---------- EJERCICIOS TRANSACCIONES ----------

    public static int VentasNoConfirmadasEnNA(IEnumerable<Venta> ventas)
    {
        var noConfirmadasNA = Filter(ventas, v =>
            v.Region.Equals("Norteamérica", StringComparison.OrdinalIgnoreCase)
            && v.Estado == Estado.Cancelada);

        // Contador con Reduce
        var conteo = Reduce<Venta, int>(noConfirmadasNA, (v, acc) => acc + 1, 0);
        return conteo;
    }

    public static decimal TotalConfirmadasEuropa(IEnumerable<Venta> ventas)
    {
        var confirmadasEU = Filter(ventas, v =>
            v.Region.Equals("Europa", StringComparison.OrdinalIgnoreCase)
            && v.Estado == Estado.Confirmada);

        var total = Reduce<Venta, decimal>(confirmadasEU, (v, acc) => acc + v.Cantidad, 0m);
        return total;
    }

    public static (string Region, decimal Neto) RegionConMayorFacturacionNeta(
        IEnumerable<Venta> ventas,
        IReadOnlyDictionary<string, decimal> margenPorRegion)
    {
        // 1) Filtra confirmadas
        var confirmadas = Filter(ventas, v => v.Estado == Estado.Confirmada);

        // 2) Proyecta a (Region, NetoPorVenta)
        var netosPorVenta = Map(confirmadas, v =>
        {
            if (!margenPorRegion.TryGetValue(v.Region, out var m))
                throw new ArgumentException($"Margen desconocido para región: {v.Region}");
            return (Region: v.Region, Neto: v.Cantidad * m);
        });

        // 3) Reduce -> acumula netos por región en un diccionario
        var netoPorRegion = Reduce<(string Region, decimal Neto), Dictionary<string, decimal>>(
            netosPorVenta,
            (item, acc) =>
            {
                acc[item.Region] = acc.TryGetValue(item.Region, out var actual)
                    ? actual + item.Neto
                    : item.Neto;
                return acc;
            },
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        );

        // 4) Selecciona la región con mayor neto
        var max = netoPorRegion.OrderByDescending(kv => kv.Value).First();
        return (max.Key, max.Value);
    }

    public static Dictionary<string, List<Venta>> AgruparPorRangos(
        IEnumerable<(decimal min, decimal max)> ranges,
        IEnumerable<Venta> ventas)
    {
        var resultado = Reduce<(decimal min, decimal max), Dictionary<string, List<Venta>>>(
            ranges,
            (rango, acc) =>
            {
                string key = $"{rango.min}-{rango.max}";
                if (!acc.ContainsKey(key))
                    acc[key] = new List<Venta>();

                // Filtra ventas con Amount en [min, max)
                var dentro = Filter(ventas, v => v.Cantidad >= rango.min && v.Cantidad < rango.max);
                foreach (var v in dentro)
                    acc[key].Add(v);

                return acc;
            },
            new Dictionary<string, List<Venta>>()
        );

        return resultado;
    }

}
public enum Estado
{
    Cancelada,
    Confirmada
}

public class Venta
{
    public required string Region { get; set; }
    public Estado Estado { get; set; }
    public decimal Cantidad { get; set; }
}

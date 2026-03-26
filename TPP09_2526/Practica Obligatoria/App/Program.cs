namespace tpp.lab09;

using System.Linq;
public static class Program
{
    private static Modelo modelo = new Modelo();

    static void Main()
    {
        EjerciciosObligatorios();
    }

    static void EjerciciosObligatorios()
    {
        Consulta1();
        Consulta2();
        Consulta3();
        Consulta4();
        Consulta5();
        Consulta6();
    }

    static void Show<T>(IEnumerable<T> secuencia)
    {
        Console.WriteLine("-----------------------------------------------");
        foreach (var elemento in secuencia)
            Console.WriteLine(elemento);
        Console.WriteLine();
    }

    static void Show<TClave, TElemento>(IEnumerable<IGrouping<TClave, TElemento>> grupos)
    {
        Console.WriteLine("-----------------------------------------------");
        foreach (var grupo in grupos)
        {
            Console.WriteLine(grupo.Key); // Un grupo consta de una clave (.Key)
            foreach (var elemento in grupo) // Y almacena los elementos asociados a la clave.
                Console.WriteLine($"\t{elemento}");
            Console.WriteLine();
        }
    }

    static void Consulta1()
    {
        // Obtener el nombre de las plataformas que tengan al menos una película dirigida por “Greta Gerwig”
        Console.WriteLine("Consulta 1");

        var consulta1 = modelo.Peliculas
                            .Where(p => p.Director == "Greta Gerwig")
                            .Select(p => p.Plataforma.Nombre);

        Show(consulta1);
    }

    static void Consulta2()
    {
        // Obtener el nombre de los directores que tengan al menos una película con más de un género.
        Console.WriteLine("Consulta 2");
        var consulta2 = modelo.Peliculas
                            .Where(p => p.Generos.Count > 1)
                            .Select(p => p.Director)
                            .Distinct();

        Show(consulta2);
    }

    static void Consulta3()
    {
        // Obtener el título de la película, la fuente y la puntuación de aquellas valoraciones 
        // cuya fuente coincida con la plataforma en la que está disponible la película,
        // ordenadas alfabéticamente por título. 
        Console.WriteLine("Consulta 3a");

        var consulta3a = modelo.Valoraciones
                            .Join(modelo.Peliculas,
                                valoracion => valoracion.IdImdb,
                                pelicula => pelicula.IdImdb,
                                (valoracion, pelicula) =>
                                new
                                {
                                    Titulo = pelicula.Titulo,
                                    Plataforma = pelicula.Plataforma.Nombre,
                                    Fuente = valoracion.Fuente,
                                    Puntuacion = valoracion.Puntuacion
                                })
                            .Where(v => v.Fuente == v.Plataforma)
                            .OrderBy(v => v.Titulo);

        Show(consulta3a);
    }

    static void Consulta4()
    {
        // Obtener, para cada plataforma, la duración mínima, máxima y media de sus películas, 
        // ordenando el resultado alfabéticamente por nombre de plataforma. 
        // Cada línea debe tener el formato: (Plataforma;Min;Max;Media)
        Console.WriteLine("Consulta 4");

        var consulta4 = modelo.Plataformas
                            .OrderBy(p => p.Nombre)
                            .Select(p => new
                            {
                                Plataforma = p.Nombre,
                                MinDuracion = p.Peliculas.Any() ? p.Peliculas.Min(p => p.Duracion) : 0,
                                MaxDuracion = p.Peliculas.Any() ? p.Peliculas.Max(p => p.Duracion) : 0,
                                MediaDuracion = p.Peliculas.Any() ? p.Peliculas.Average(p => p.Duracion) : 0
                            });
        
        Show(consulta4);
    }

    static void Consulta5()
    {
        // Obtener, para cada director que tenga películas estrenadas a partir de 2010 con valoraciones,
        // el nombre del director y la suma de votos de las valoraciones de dichas películas. 
        Console.WriteLine("Consulta 5");

        var consulta5 = modelo.Peliculas
                            .Where(p => p.FechaEstreno.Year >= 2010)
                            .Join(modelo.Valoraciones,
                                pelicula => pelicula.IdImdb,
                                valoracion => valoracion.IdImdb,
                                (pelicula, valoracion) =>
                                new
                                {
                                    Director = pelicula.Director,
                                    Votos = valoracion.Votos
                                })
                            .GroupBy(p => p.Director)
                            .Select(g => new
                            {
                                Director = g.Key,
                                TotalVotos = g.Sum(p => p.Votos)
                            })
                            .OrderByDescending(d => d.TotalVotos);

        Show(consulta5);
    }

    static void Consulta6()
    {
        // Obtener el título y la duración de las películas cuya duración sea superior 
        // a la duración media de las películas del modelo.
        Console.WriteLine("Consulta 6");


        var consulta6 = modelo.Peliculas
                            .Where(p => p.Duracion > modelo.Peliculas.Average(p => p.Duracion))
                            .Select(p => new
                            {
                                Titulo = p.Titulo,
                                Duracion = p.Duracion
                            })
                            .OrderByDescending(p => p.Duracion);

        Show(consulta6);
    }
}

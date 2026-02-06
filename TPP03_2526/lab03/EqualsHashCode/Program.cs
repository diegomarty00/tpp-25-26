namespace EqualsHashCode;

/// <summary>
/// Equals ¿qué comprueba?
/// HashCode ¿para qué sirve? Dos opciones:
///     (valor1, valor2, valor3).GetHashCode(); //Son tuplas, se pueden usar para comparar objetos con varios campos
///     HashCode.Combine(valor1, valor2, valor3)
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Song cancion1 = new Song { Title ="Pearl Jam - Blood", Artist = "Pearl Jam"};
        Song cancion2 = cancion1;
        Song cancion3 = new Song { Title ="Pearl Jam - Blood", Artist = "Pearl Jam"};
        Song cancion4 = new Song { Title ="Pearl Jam - Blood", Artist = "Mi artista"};
        Console.WriteLine($"¿Son iguales las canciones 1 y 2? {Equals(cancion1, cancion2)}");
        Console.WriteLine($"¿Son iguales las canciones 1 y 3? {Equals(cancion1, cancion3)}");
        Console.WriteLine($"¿Son iguales las canciones 1 y 4? {Equals(cancion1, cancion4)}");
    }

}

/// <summary>
/// Añade una propiedad Artist, modifica el código del Main. 
/// Modifica la clase para que tenga los métodos Equals y GetHashCode
/// ¿Es aconsejable que sean de sólo lectura?
/// </summary>
class Song()
{
    public required string Title {get; init;}

    public required string Artist {get; init;}

     public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Song other = (Song)obj;
        return Title == other.Title && Artist == other.Artist;
    }

    public override int GetHashCode()
    {
        return (Title, Artist).GetHashCode();
    }
    
}

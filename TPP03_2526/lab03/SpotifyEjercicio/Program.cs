namespace SpotifyEjercicio;


/// <summary>
/// Replantea código del proyecto Spotify empleando ISP.
/// </summary>
class Program
{
    static void Main()
    {
        var spotify = new Spotify();
        var song1 = new Song("Canción 1");
        var song2 = new Song("Canción 2");
        var podcast1 = new Podcast("Podcast 1");
        var radio1 = new Radio("Radio 1");
 
    // Combinaciones de las anteriores en distintos arrays con distintos tipos...
        Console.WriteLine("Reproducir todos los audios:");
        spotify.PlayAll(new IPlayable[] { song1, podcast1, radio1 });
        Console.WriteLine("Me gustan, vamos a descargarlos todos:");
        spotify.DownloadAll(new IDownloadable[] { song1, podcast1 });
        Console.WriteLine("Serializar todos los metadatos:");
        spotify.SerializeAll(new ISerializable[] { song1, podcast1, radio1 });
    }
}

class Spotify 
{
    public void PlayAll(IPlayable[] library)
    {
        foreach (var audio in library)
        {
            audio.Play();
        }
    }

    public void DownloadAll(IDownloadable[] library)
    {
        foreach (var audio in library)
        {
            audio.Download();
        }
    }

    public void SerializeAll(ISerializable[] library)
    {
        foreach (var audio in library)
        {
            Console.WriteLine(audio.Serialize());
        }
    }
}

class Song : IPlayable, IDownloadable, ISerializable
{

    public string Name { get; }

    public Song(string name)
    {
        Name = name;
    }

    public void Play()
    {
        Console.WriteLine($"Reproduciendo canción '{Name}'...");
    }
    
    public void Download()
    {
        Console.WriteLine($"Descargando canción'{Name}'...");
    }

    public string Serialize()
    {
        return $"Metadatos serialziados: {Name}";
    }
}

class Podcast : IPlayable, IDownloadable, ISerializable
{
    public string Name { get; }

    public Podcast(string name)
    {
        Name = name;
    }

    public void Play()
    {
        Console.WriteLine($"Reproduciendo podcast '{Name}'...");
    }

    public void Download()
    {
        Console.WriteLine($"Descargando Podcast  podcast '{Name}'...");
    }

    public string Serialize()
    {
        return $"Metadatos serialziados: {Name}";
    }
}

class Radio : IPlayable, ISerializable
{
    
    public string Name { get; }

    public Radio(string name)
    {
        Name = name;
    }

    public void Play()
    {
        Console.WriteLine($"Reproduciendo radio '{Name}'...");
    }

    public string Serialize()
    {
        return $"Metadatos serialziados: {Name}";
    }
}

interface IDownloadable { void Download(); }
interface ISerializable { string Serialize(); }
interface IPlayable { void Play(); }


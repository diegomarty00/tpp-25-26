namespace Personas;

public class Client
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Client(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name} ({Age} años)";
    }
}

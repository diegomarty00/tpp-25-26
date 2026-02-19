namespace Console_App;
using Listas;
class Program
{
    static void Main(string[] args)
    {

        var lista = new LinkedList<int>();
        
        lista.Add(10);
        lista.Add(20);
        lista.Add(30);

        foreach (var x in lista)
        {
            Console.WriteLine(x);
        }

    }
}

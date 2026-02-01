namespace Consola_App;

using Lista_Inmutable;

class Program
{
    static void Main(string[] args)
    {
        var list1 = new InmutableList();   // lista vacía 
        var list2 = list1.Add("1");        // list1 sigue vacía, list2 = ["1"] 
        var list3 = list2.Add(null);       // list2 = ["1"], list3 = ["1", null] 
        var list4 = list3.Add(2);          // list3 = ["1", null], list4 = ["1", null, 2] 
        if (list4.Contains(null))          // se imprime "TRUE" 
            Console.WriteLine("TRUE");
        var list5 = list4.Remove(null);    // list4 = ["1", null, 2], list5 = ["1", 2] 

        Console.WriteLine(list1.Count);    // 0 
        Console.WriteLine(list1.ToString()); // Lista_Inmutable.InmutableList
        Console.WriteLine(list2.Count);    // 1 
        Console.WriteLine(list2.ToString()); // Lista_Inmutable.InmutableList
        Console.WriteLine(list3.Count);    // 2 
        Console.WriteLine(list3.ToString()); // Lista_Inmutable.InmutableList
        Console.WriteLine(list4.Count);    // 3 
        Console.WriteLine(list4.ToString()); // Lista_Inmutable.InmutableList
        Console.WriteLine(list5.Count);    // 2 
        Console.WriteLine(list5.ToString()); // Lista_Inmutable.InmutableList

    }
}

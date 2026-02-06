namespace Library;

public class Cliente
{
    string Id { get; set; }
    public string Nombre { get; set; } //init es como set pero da inmutabilidad despues de la inicializacion
    string Apellido { get; set; }
    int _edad;
    public int Edad { 
        get{
            return _edad;
            }
        set
        {
            if (value < 0)
                _edad = 0;
            else
                _edad = value;
        } }
    enum Status {VIP, Business, Standard}

    public Cliente() {}
    public Cliente(string Nombre, string Apellido)
    {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
    }

    public override string ToString()
    {
        return $"Hola, {Nombre}!";
    }
}

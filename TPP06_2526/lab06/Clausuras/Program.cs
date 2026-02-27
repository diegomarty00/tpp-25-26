namespace lab06;

class Program
{
    static void Main()
    {
        //EjemploClausurasEncapsulacionEstado();
        EjemploClausurasEncapsulacionEstadoDepositoExtraccion();
    }

    private static void EjemploClausurasEncapsulacionEstado()
    {
        Func<decimal,decimal> depositar = Cuenta(1000m);
        Console.WriteLine($"Depositar 100: {depositar(100m)}");
    }

    private static void EjemploClausurasEncapsulacionEstadoDepositoExtraccion()
    {
        var (depositar, extraer) = CuentaDepositoExtraccion(1000m);
        Console.WriteLine($"Depositar 100: {depositar(100m)}");
        Console.WriteLine($"Extraer 50: {extraer(50m)}");
        Console.WriteLine($"Extraer 200: {extraer(200m)}");
    }

    static Func<decimal, decimal> Cuenta(decimal inicial)
    {
        decimal balance = inicial; //variable local que será capturada ¿Por qué?
        decimal depositar(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad a depositar debe ser positiva");
            }
            balance += cantidad; 
            return balance;
        }
        // Al devolver el delegado, el estado capturado sigue vivo mientras exista esta referencia.
        // En este caso, el estado capturado lo define exclusivamente la variable 'balance'.
        return depositar;
    }

    // EJERCICIO:
    // Imagina que, dentro de Cuenta, quieres devolver dos clausuras para una misma cuenta:
    //  - Una clausura para depositar.
    //  - Otra clausura para extraer.
    // Ambas deben trabajar sobre el mismo estado capturado. Impleméntalo
    public static (Func<decimal, decimal> Depositar, Func<decimal, decimal> Extraer) CuentaDepositoExtraccion(decimal inicial)
    {
        decimal balance = inicial;
        decimal depositar(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad a depositar debe ser positiva");
            }
            balance += cantidad; 
            return balance;
        }
        decimal extraer(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad a extraer debe ser positiva");
            }
            if (cantidad > balance)
            {
                throw new InvalidOperationException("No hay suficiente saldo para extraer esa cantidad");
            }
            balance -= cantidad; 
            return balance;
        }
        return (depositar, extraer);
    }
}

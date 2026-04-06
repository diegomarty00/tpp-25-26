using System;

public class ForLoop
{
    public static void Run(
        Action initialize,
        Func<bool> condition,
        Action iteration,
        Action body)
    {
        if (initialize == null || condition == null || iteration == null || body == null)
            throw new ArgumentNullException("Uno de los parámetros de ForLoop.Run es nulo.");

        initialize();

        ExecuteRecursive(condition, iteration, body);
    }

    private static void ExecuteRecursive(
        Func<bool> condition,
        Action iteration,
        Action body)
    {
        // Condición de parada
        if (!condition())
            return;

        // Cuerpo del bucle
        body();

        // Iteración
        iteration();

        // Recursión
        ExecuteRecursive(condition, iteration, body);
    }
}
namespace Memoization;

using System;
using System.Collections.Generic;
using System.Numerics;

public sealed class MemoizedCalculator<T> where T : INumber<T>
{
    public T Result { get; private set; } = T.Zero;

    private readonly Dictionary<(char op, T a, T b), T> _cache = new();
    
    // Normaliza la clave para operaciones conmutativas (suma y multiplicación) 
    // para evitar almacenar resultados duplicados.
    private static (char op, T a, T b) NormalizeKey(char op, T a, T b)
    {
        if (op is '+' or '*')
        {
            if (Comparer<T>.Default.Compare(a, b) > 0)
            {
                (a, b) = (b, a);
            }
        }
        return (op, a, b);
    }

    private T Evaluate(char op, T a, T b, Func<T, T, T> compute)
    {
        var key = NormalizeKey(op, a, b);

        if (_cache.TryGetValue(key, out var value))
        {
            Result = value;
            return value;
        }

        var result = compute(a, b);
        _cache[key] = result;
        Result = result;
        return result;
    }

    public T Add(T a, T b) => Evaluate('+', a, b, static (x, y) => x + y);

    public T Subtract(T a, T b) => Evaluate('-', a, b, static (x, y) => x - y);

    public T Multiply(T a, T b) => Evaluate('*', a, b, static (x, y) => x * y);

    public T Divide(T a, T b)
    {
        if (b == T.Zero)
            throw new DivideByZeroException("No se puede dividir por cero.");
        return Evaluate('/', a, b, static (x, y) => x / y);
    }

    public void Clear() => _cache.Clear();
}

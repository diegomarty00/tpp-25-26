using System;

namespace Maybe;

public sealed class Some<TData> : Maybe<TData>
{
    private readonly TData value;

    public Some(TData value)
    {
        this.value = value;
    }

    public void Match(Action<TData> someAction, Action noneAction)
    {
        someAction(value);
    }

    public TResult Match<TResult>(Func<TData, TResult> some, Func<TResult> none)
    {
        return some(value);
    }

    public Maybe<TResult> AndThen<TResult>(Func<TData, TResult> func)
    {
        return new Some<TResult>(func(value));
    }

    public TData OrElse(TData defaultValue)
    {
        return value;
    }

    public override string ToString() => $"Some({value})";
}
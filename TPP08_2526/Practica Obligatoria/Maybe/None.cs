using System;

namespace Maybe;

public sealed class None<TData> : Maybe<TData>
{
    public void Match(Action<TData> someAction, Action noneAction)
    {
        noneAction();
    }

    public TResult Match<TResult>(Func<TData, TResult> some, Func<TResult> none)
    {
        return none();
    }

    public Maybe<TResult> AndThen<TResult>(Func<TData, TResult> func)
    {
        return new None<TResult>();
    }

    public TData OrElse(TData defaultValue)
    {
        return defaultValue;
    }

    public override string ToString() => "None";
}
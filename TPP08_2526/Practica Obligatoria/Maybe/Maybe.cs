namespace Maybe;

public interface Maybe<TData>
{
    void Match(Action<TData> someAction, Action noneAction);

    TResult Match<TResult>(Func<TData, TResult> some, Func<TResult> none);

    Maybe<TResult> AndThen<TResult>(Func<TData, TResult> func);

    TData OrElse(TData defaultValue);
}
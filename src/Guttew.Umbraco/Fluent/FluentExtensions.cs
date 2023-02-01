namespace Guttew.Umbraco.Fluent;

public static class FluentExtensions
{
    public static T If<T>(this T source, bool condition, Func<T, T> func)
    {
        return condition
            ? func(source)
            : source;
    }

    public static T If<T>(this T source, Func<bool> condition, Func<T, T> func)
    {
        return If(source, condition(), func);
    }

    public static T If<T>(this T source, Func<T, bool> condition, Func<T, T> func)
    {
        return If(source, condition(source), func);
    }

    public static T IfNot<T>(this T source, bool condition, Func<T, T> func)
    {
        return If(source, !condition, func);
    }

    public static T IfNot<T>(this T source, Func<bool> condition, Func<T, T> func)
    {
        return IfNot(source, condition(), func);
    }

    public static T IfNot<T>(this T source, Func<T, bool> condition, Func<T, T> func)
    {
        return IfNot(source, condition(source), func);
    }

    public static T Fluent<T>(this T source, Action<T> action)
    {
        action(source);
        return source;
    }
}

using System;

public static class Extensions
{
    public static T? Find<T>(this T[] value, Func<T, bool> predicate)
    {
        foreach (var item in value)
            if (predicate(item)) return item;
        
        return default(T);
    }
}

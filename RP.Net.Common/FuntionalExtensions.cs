using System;

namespace RP.Net.Common
{
    public static class FuntionalExtensions
    {
        public static T When<T>(this T @this, Func<bool> predicate, Func<T, T> func)
            => predicate() ? func(@this) : @this;

        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }
    }
}

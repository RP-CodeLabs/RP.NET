namespace RP.Net.Common
{
    public class Maybe<T> where T : class
    {
        public static Maybe<T> None = new Maybe<T>(default(T));

        public Maybe(T value) => Value = value;
        public T Value { get; }

        public bool HasValue => Value != null && !Value.Equals(default(T));

        public bool HasNoValue => !HasValue;

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);
    }
}
namespace RP.Net.Common
{
    public abstract class ValueObject<TType> where TType : class 
    {
        public static bool operator ==(ValueObject<TType> a, ValueObject<TType> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return false;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<TType> a, ValueObject<TType> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as TType;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        public override int GetHashCode() => GetHashCodeCore();
        protected abstract bool EqualsCore(TType other);

        protected abstract int GetHashCodeCore();
    }
}
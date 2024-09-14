namespace Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(T? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (ReferenceEquals(null, other)) return false;

        using var thisValues = GetAtomicValues().GetEnumerator();
        using var otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current != otherValues.Current)
            {
                return false;
            }
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override bool Equals(object? obj)
    {
        if (obj is T valueObject)
        {
            return Equals(valueObject);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return GetAtomicValues().Aggregate(0, (current, value) => (current * 397) ^ (value.GetHashCode()));
    }

    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !Equals(left, right);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>, IEqualityComparer<ValueObject>
{
    public abstract List<Object> GetAtomicValues();

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public bool Equals(ValueObject? other)
    {
        if (other is not ValueObject) 
            return false;

        return ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is ValueObject)
            return Equals(obj as ValueObject);
        else
            return false;
    }

    public bool Equals(ValueObject? x, ValueObject? y)
    {
        return x.ValuesAreEqual(y);
    }

    public int GetHashCode([DisallowNull] ValueObject obj)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(ValueObject? one, ValueObject? other)
    {

        if (one is null && other is null)
            return true;

        if (!one.GetType().Equals(other.GetType()))
            return false;

        var equal = one.ValuesAreEqual(other);
        return equal;
    }

    public static bool operator !=(ValueObject? one, ValueObject? other)
    {
        var equal = (one == other);

        return !equal;
    }
}


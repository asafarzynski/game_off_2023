using System;
using System.Collections.Generic;

namespace GameOff2023.Scripts.GameplayCore.Id;

public readonly struct ID<TObject, TId> : IEquatable<ID<TObject, TId>>
{
    public readonly TId Id;
    public readonly Type ObjectType;

    public ID(TId id)
    {
        Id = id;
        ObjectType = typeof(TObject);
    }

    public bool Equals(ID<TObject, TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(Id, other.Id)
               && ObjectType == other.ObjectType;
    }

    public override bool Equals(object obj)
    {
        return obj is ID<TObject, TId> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, ObjectType);
    }

    public override string ToString()
    {
        return $"<ID:{Id}>";
    }
}

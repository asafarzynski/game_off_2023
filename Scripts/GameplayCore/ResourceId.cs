using System;

namespace GameOff2023.Scripts.GameplayCore;

/// <summary>
/// This should be used to link core data with visual representation.
/// <br/>
/// <br/>
/// Example: A spell has an image, a VFX, etc. but the core should not care about such things,
/// because it only cares about DATA. But still - data and visuals need to be linked somehow,
/// and here comes the ResourceId.
/// </summary>
public readonly struct ResourceId : IEquatable<ResourceId>
{
    public readonly string BaseName { get; }
    public readonly int Number { get; }
    
    public ResourceId(string baseName, int number)
    {
        BaseName = baseName;
        Number = number;
    }

    public override string ToString()
    {
        return $"<{BaseName}:{Number}>";
    }

    public bool Equals(ResourceId other)
    {
        return BaseName == other.BaseName && Number == other.Number;
    }

    public override bool Equals(object obj)
    {
        return obj is ResourceId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BaseName, Number);
    }
}
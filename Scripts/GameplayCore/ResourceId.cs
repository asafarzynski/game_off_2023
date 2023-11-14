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
    private readonly string _baseName;
    private readonly int _number;
    
    public ResourceId(string baseName, int number)
    {
        _baseName = baseName;
        _number = number;
    }

    public override string ToString()
    {
        return $"<{_baseName}:{_number}>";
    }

    public bool Equals(ResourceId other)
    {
        return _baseName == other._baseName && _number == other._number;
    }

    public override bool Equals(object obj)
    {
        return obj is ResourceId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_baseName, _number);
    }
}
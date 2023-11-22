using Godot;

namespace GameOff2023.Scripts.Resources.Interfaces;

/// <summary>
/// Used for all resources containing icon for the object their represent
/// </summary>
public interface IIconProvider
{
    public Texture2D Icon { get; }
}
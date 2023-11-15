using Godot;

namespace GameOff2023.Scripts.Resources;

/// <summary>
/// Use float / int / bool depending on modifier implementation
/// </summary>
public abstract partial class SpellModifierElementResource : Resource
{
    [Export] public float FloatValue;
    [Export] public int IntValue;
    [Export] public bool BoolValue;

    public SpellModifierElementResource() : this(0, 0, false) {}

    public SpellModifierElementResource(float floatValue, int intValue, bool boolValue)
    {
        FloatValue = floatValue;
        IntValue = intValue;
        BoolValue = boolValue;
    }
}
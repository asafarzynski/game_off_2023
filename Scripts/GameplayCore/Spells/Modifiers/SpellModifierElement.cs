using Godot;

namespace GameOff2023.Scripts.GameplayCore.Spells;

/// <summary>
/// Use float / int / bool depending on modifier implementation
/// </summary>
public abstract partial class SpellModifierElement : Resource
{
    [Export]
    public float FloatValue;

    [Export]
    public int IntValue;

    [Export]
    public bool BoolValue;

    public SpellModifierElement()
        : this(0, 0, false) { }

    public SpellModifierElement(float floatValue, int intValue, bool boolValue)
    {
        FloatValue = floatValue;
        IntValue = intValue;
        BoolValue = boolValue;
    }

    public abstract Spell Modify(Spell spell);
}

using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.Resources;

/// <summary>
/// Use float / int / bool depending on modifier implementation
/// </summary>
public abstract partial class SpellModifierElementResource : Resource
{
    [Export] protected float FloatValue;
    [Export] protected int IntValue;
    [Export] protected bool BoolValue;

    public SpellModifierElementResource() : this(0, 0, false) { }

    public SpellModifierElementResource(float floatValue, int intValue, bool boolValue)
    {
        FloatValue = floatValue;
        IntValue = intValue;
        BoolValue = boolValue;
    }

    protected abstract Spell Modify(Spell spell);

    public SpellModifierElement ToModifierElement()
    {
        return new SpellModifierElement(FloatValue, IntValue, BoolValue, Modify);
    }
}

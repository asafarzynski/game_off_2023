using System;

namespace GameOff2023.Scripts.GameplayCore.Spells;

/// <summary>
/// Use float / int / bool depending on modifier implementation
/// </summary>
public class SpellModifierElement
{
    private float FloatValue { get; }

    private int IntValue { get; }

    private bool BoolValue { get; }

    public readonly Func<Spell, Spell> Modify;

    public SpellModifierElement()
        : this(0, 0, false, (spell) => spell) { }

    public SpellModifierElement(float floatValue, int intValue, bool boolValue, Func<Spell, Spell> modifyingFunction)
    {
        FloatValue = floatValue;
        IntValue = intValue;
        BoolValue = boolValue;
        Modify = modifyingFunction;
    }
}

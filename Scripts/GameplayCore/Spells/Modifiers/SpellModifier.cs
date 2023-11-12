using Godot;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public partial class SpellModifier : Resource
{
    [Export]
    public SpellModifierElement[] ModifierElements;

    public Spell ApplyModifier(Spell spellBase)
    {
        var result = spellBase;

        foreach (SpellModifierElement spellModifierElement in ModifierElements)
        {
            result = spellModifierElement.Modify(result);
        }

        return result;
    }
}

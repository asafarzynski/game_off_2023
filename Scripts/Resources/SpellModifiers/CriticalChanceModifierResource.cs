using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.Resources.SpellModifiers;

public partial class CriticalChanceModifierResource : SpellModifierElementResource
{
    protected override Spell Modify(Spell spell)
    {
        var result = spell;
        result.CriticalChance *= FloatValue;
        return result;
    }
}

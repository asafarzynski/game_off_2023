using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.Resources.SpellModifiers;

public partial class DamageModifierResource : SpellModifierElementResource
{
    protected override Spell Modify(Spell spell)
    {
        var result = spell;
        result.Damage *= FloatValue;
        return result;
    }
}

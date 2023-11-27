using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.Resources.SpellModifiers;

public partial class CooldownModifierResource : SpellModifierElementResource
{
    protected override Spell Modify(Spell spell)
    {
        var result = spell;
        result.Cooldown = Mathf.RoundToInt(result.Cooldown * FloatValue);
        return result;
    }
}

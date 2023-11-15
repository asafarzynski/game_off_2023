namespace GameOff2023.Scripts.GameplayCore.Spells;

/// <summary>
/// Use float to multiply Damage
/// </summary>
public partial class DamageModifierElement : SpellModifierElement
{
    public override Spell Modify(Spell spell)
    {
        return spell with { Damage = spell.Count * FloatValue };
    }
}

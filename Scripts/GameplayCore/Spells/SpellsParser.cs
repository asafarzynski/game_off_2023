using System.Collections.Generic;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public static class SpellsParser
{
    public static Spell ApplyModifiers(this Spell spellBase, List<SpellModifier> modifiers)
    {
        var result = spellBase;

        foreach (SpellModifier spellModifier in modifiers)
        {
            spellBase = spellModifier.ApplyModifier(spellBase);
        }
        
        return result;
    }
}
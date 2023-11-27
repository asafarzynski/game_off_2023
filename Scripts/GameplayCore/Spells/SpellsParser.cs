namespace GameOff2023.Scripts.GameplayCore.Spells;

public static class SpellsParser
{
    public static Spell ApplyModifiers(this Spell spellBase, SpellModifier?[] modifiers)
    {
        var result = spellBase;

        foreach (var spellModifier in modifiers)
        {
            if (spellModifier == null)
                continue;
            
            spellBase = spellModifier.Value.ApplyModifier(spellBase);
        }

        return result;
    }
}

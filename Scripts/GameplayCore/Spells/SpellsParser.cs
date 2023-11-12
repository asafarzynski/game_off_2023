namespace GameOff2023.Scripts.GameplayCore.Spells;

public static class SpellsParser
{
	public static Spell ApplyModifiers(this SpellSlot spellSlot)
	{
		return ApplyModifiers(spellSlot.Spell, spellSlot.Modifiers);
	}
	
	public static Spell ApplyModifiers(this Spell spellBase, SpellModifier[] modifiers)
	{
		var result = spellBase;

		foreach (SpellModifier spellModifier in modifiers)
		{
			spellBase = spellModifier.ApplyModifier(spellBase);
		}
		
		return result;
	}
}

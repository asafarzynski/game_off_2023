namespace GameOff2023.Scripts.GameplayCore.Spells;

public class SpellSlot
{
    public Spell Spell;
    public SpellModifier[] Modifiers;

    public const int MAX_NUMBER_OF_MODIFIERS = 4;
    
    public SpellSlot(Spell spell)
    {
        Spell = spell;
        Modifiers = new SpellModifier[MAX_NUMBER_OF_MODIFIERS];
    }
}
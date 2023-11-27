using GameOff2023.Scripts.GameplayCore.Inventory;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public struct SpellModifier : IInventoryItem
{
    public SpellModifierElement[] ModifierElements { get; }

    public ResourceId ResourceId { get; }

    public SpellModifier(ResourceId resourceId, SpellModifierElement[] elements)
    {
        ResourceId = resourceId;
        ModifierElements = elements;
    }

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

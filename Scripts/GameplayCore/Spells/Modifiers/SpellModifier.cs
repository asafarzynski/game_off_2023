using GameOff2023.Scripts.GameplayCore.Inventory;
using Godot;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public partial class SpellModifier : Resource, IInventoryItem
{
    [Export] public SpellModifierElement[] ModifierElements;

    public ResourceId ResourceId { get; } // TODO: fill it

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
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Inventory;

public class Inventory
{
    public SpellSlot[] SpellSlots { get; private set; }
    public IInventoryItem[] LooseSlots { get; private set; }

    private const int SPELL_SLOTS_NUMBER = 4;
    private const int INVENTORY_SIZE = 12;

    public Inventory()
    {
        SpellSlots = new SpellSlot[SPELL_SLOTS_NUMBER];
        for (int i = 0; i < SPELL_SLOTS_NUMBER; i++)
        {
            SpellSlots[i] = new();
        }
        LooseSlots = new IInventoryItem[INVENTORY_SIZE];
    }

    public void AddNewItem(IInventoryItem item)
    {
        for (var i = 0; i < LooseSlots.Length; i++)
        {
            if (LooseSlots[i] == null)
            {
                LooseSlots[i] = item;
                return;
            }
        }
    }

    public List<Spell> ParseSpells()
    {
        var result = new List<Spell>();
        foreach (var spellSlot in SpellSlots)
        {
            if (spellSlot == null || !spellSlot.Spell.HasValue)
                continue;
            
            result.Add(spellSlot.GetModifiedSpell());
        }
        return result;
    }
}

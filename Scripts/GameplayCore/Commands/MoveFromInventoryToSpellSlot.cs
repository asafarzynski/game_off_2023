using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Inventory;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class MoveFromInventoryToSpellSlot : GameplayCoreCommand
{
    private readonly int _fromInventorySlot;
    private readonly int _toSpellSlot;
    private readonly int? _toModifierSlot;

    private IInventoryItem _replacedItem;

    public MoveFromInventoryToSpellSlot(GameplayCore gameplayCore, int fromInventorySlot, int toSpellSlot, int? toModifierSlot) : base(gameplayCore)
    {
        _fromInventorySlot = fromInventorySlot;
        _toSpellSlot = toSpellSlot;
        _toModifierSlot = toModifierSlot;
    }

    public override CommandValidation Validate()
    {
        if (_fromInventorySlot >= Core.Inventory.LooseSlots.Length || _fromInventorySlot < 0)
            return CommandValidationCreator.Invalid("inventory slot index out of bounds");

        if (_toSpellSlot >= Core.Inventory.SpellSlots.Length || _toSpellSlot < 0)
            return CommandValidationCreator.Invalid("spell slot index out of bounds");

        if (_toModifierSlot.HasValue)
        {
            if (_toModifierSlot.Value >= Core.Inventory.SpellSlots[_toSpellSlot].Modifiers.Length || _toModifierSlot.Value < 0)
                return CommandValidationCreator.Invalid("spell modifier index out of bounds");
        }

        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        var inventoryItem = Core.Inventory.LooseSlots[_fromInventorySlot];

        var spellSlot = Core.Inventory.SpellSlots[_toSpellSlot];

        switch (inventoryItem)
        {
            case SpellModifier spellModifier:
            {
                if (_toModifierSlot.HasValue)
                {
                    _replacedItem = spellSlot.Modifiers[_toModifierSlot.Value];
                    spellSlot.Modifiers[_toModifierSlot.Value] = spellModifier;
                }
                else
                {
                    _replacedItem = spellSlot.Modifiers[^1];
                    spellSlot.Modifiers[^1] = spellModifier;  
                }
                break;
            }
            case Spell spell:
            {
                _replacedItem = spellSlot.Spell;
                spellSlot.Spell = spell;
                break;
            }
            default:
            {
                if (_toModifierSlot.HasValue)
                {
                    _replacedItem = spellSlot.Modifiers[_toModifierSlot.Value];
                    spellSlot.Modifiers[_toModifierSlot.Value] = null;
                }
                else
                {
                    _replacedItem = spellSlot.Spell;
                    spellSlot.Spell = null;
                }
                break;
            }
        }

        Core.Inventory.LooseSlots[_fromInventorySlot] = _replacedItem;
        
        Core.Events.OnInventoryChanged?.Invoke();
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

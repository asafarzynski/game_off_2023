using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Inventory;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class MoveElementToSpellSlot : ICommand
{
    private readonly GameplayCore _gameplayCore;
    private readonly int _fromInventorySlot;
    private readonly int _toSpellSlot;
    private readonly int? _toModifierSlot;

    private IInventoryItem _replacedItem;

    public MoveElementToSpellSlot(GameplayCore gameplayCore, int fromInventorySlot, int toSpellSlot, int? toModifierSlot)
    {
        _gameplayCore = gameplayCore;
        _fromInventorySlot = fromInventorySlot;
        _toSpellSlot = toSpellSlot;
        _toModifierSlot = toModifierSlot;
    }

    public bool Validate()
    {
        if (_fromInventorySlot >= _gameplayCore.Inventory.LooseSlots.Length || _fromInventorySlot < 0)
            return false;

        if (_toSpellSlot >= _gameplayCore.Inventory.SpellSlots.Length || _toSpellSlot < 0)
            return false;

        return true;
    }

    public void Execute()
    {
        var movedElement = _gameplayCore.Inventory.LooseSlots[_fromInventorySlot];

        var spellSlot = _gameplayCore.Inventory.SpellSlots[_toSpellSlot];

        switch (movedElement)
        {
            case SpellModifier spellModifier when _toModifierSlot.HasValue:
            {
                _replacedItem = spellSlot.Modifiers[_toModifierSlot.Value];
                spellSlot.Modifiers[_toModifierSlot.Value] = spellModifier;
                break;
            }
            case SpellModifier spellModifier:
            {
                _replacedItem = spellSlot.Modifiers[^1];
                spellSlot.Modifiers[^1] = spellModifier;
                break;
            }
            case Spell spell:
            {
                _replacedItem = spellSlot.Spell;
                spellSlot.Spell = spell;
                break;
            }
        }

        if (_replacedItem != null)
        {
            _gameplayCore.Inventory.LooseSlots[_fromInventorySlot] = _replacedItem;
        }
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Player;

public class PlayerInventory
{
    private SpellSlot[] _spellSlots;

    private const int SPELL_SLOTS_NUMBER = 6;

    public PlayerInventory()
    {
        _spellSlots = new SpellSlot[SPELL_SLOTS_NUMBER];
    }
}

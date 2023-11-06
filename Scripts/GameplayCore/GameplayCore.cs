using System.Collections.Generic;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
    public readonly CommandsExecutioner CommandsExecutioner = new();
    
    public float Score { get; internal set; } = 1f;
    
    public List<Spell> AvailableSpells { get; private set; }

    public GameplayCore(List<Spell> spells)
    {
        AvailableSpells = spells;
    }
}
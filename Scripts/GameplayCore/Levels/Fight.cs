using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Levels;

public enum FightStatus
{
    Win,
    Lose,
    Draw,
    InProgress
}

public enum FightEventType
{
    FightStart,
    FightResult,
    SpellCast,
    CharacterDeath
}

public class FightEvent
{
    public FightEventType EventType { get; set; }
    public FightStatus? FightResult { get; set; }
    public SpellCast SpellCast { get; set; }
    public FightingCharacter TargetCharacter { get; set; }
    public CharacterFightStatus TargetCharacterStatsAfter { get; set; }
}

public struct CharacterFightStatus
{
    public float Health { get; set; }
    // Other effect properties...
    
    public CharacterFightStatus(CharacterStats stats)
    {
        Health = stats.Health;
    }
}

public class FightingCharacter
{
    public ID<FightingCharacter, int> Id;
    public Character Character;
    public CharacterFightStatus FightStatus;
}

public class Fight
{
    public FightStatus? FightStatus { get; internal set; }
    
    public readonly int FightNumber;
    public readonly FightingCharacter[] EnemyList;

    public List<FightEvent> FightEvents;

    public Fight(int fightNumber) : this(fightNumber, new FightingCharacter[4]) { }

    public Fight(int fightNumber, FightingCharacter[] enemyList)
    {
        FightNumber = fightNumber;
        EnemyList = enemyList;
    }
}

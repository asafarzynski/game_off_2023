using System.Collections.Generic;
using GameOff2023.Scripts.Fight;
using GameOff2023.Scripts.GameplayCore.Enemies;

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
    public Character TargetCharacter { get; set; }
    public CharacterFightStatus TargetCharacterFightStatus { get; set; }
}

public class CharacterFightStatus
{
    public int Health { get; set; }
    // Other effect properties...
}

public class Fight
{
    public bool IsCleared { get; internal set; }
    
    public readonly int FightNumber;
    public readonly Enemy?[] EnemyList;

    public List<FightEvent> FightEvents;

    public Fight(int fightNumber) : this(fightNumber, new Enemy?[4]) { }

    public Fight(int fightNumber, Enemy?[] enemyList)
    {
        FightNumber = fightNumber;
        EnemyList = enemyList;
    }
}

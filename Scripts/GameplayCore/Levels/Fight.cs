using GameOff2023.Scripts.GameplayCore.Enemies;

namespace GameOff2023.Scripts.GameplayCore.Levels;

public class Fight
{
    public bool IsCleared { get; internal set; }
    
    public readonly int FightNumber;
    public readonly Enemy?[] EnemyList;

    public Fight(int fightNumber) : this(fightNumber, new Enemy?[4]) { }

    public Fight(int fightNumber, Enemy?[] enemyList)
    {
        FightNumber = fightNumber;
        EnemyList = enemyList;
    }
}

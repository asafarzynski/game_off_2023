using Godot;
using GameOff2023.Scripts.GameplayCore.Enemies;
namespace GameOff2023.Scripts.GameplayCore.Levels;

public class Fight
{
    public int FightNumber;
    public Enemy[] EnemyList;

    public Fight() : this(0, new Enemy[4]) { }

    public Fight(int fightNumber, Enemy[] enemyList)
    {
        FightNumber = fightNumber;
        EnemyList = enemyList;
    }
}

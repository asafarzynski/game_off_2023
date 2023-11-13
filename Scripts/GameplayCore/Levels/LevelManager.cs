using Godot;
using System.Collections.Generic;

using GameOff2023.Scripts.GameplayCore.Enemies;
namespace GameOff2023.Scripts.GameplayCore.Levels;

public class LevelManager
{
	public List<Enemy> Enemies { get; private set; }
	
	public LevelManager(List<Enemy> enemies){
		Enemies = enemies;
	}
	
	public Level GenerateLevel()
	{
		Level level = new Level();
		
		for (int x = 0; x < level.FightList.Length;x++)
		{
			Fight fight = new Fight();
			for (int y = 0; y < fight.EnemyList.Length;y++)
			{
				var random_generator = new RandomNumberGenerator();
				random_generator.Randomize();
				var random_value = random_generator.RandiRange(0,Enemies.Count-1);
				
				fight.EnemyList[y] = Enemies[random_value];
			}
			level.FightList[x] = fight;
		}
		return level;
	}
}

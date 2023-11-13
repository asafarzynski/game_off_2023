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
		
		for (int x = 0; x > level.FightList.Lenght;x++)
		{
			Fight fight = new Fight();
			for (int y = 0; y > fight.EnemyList.Lenght;y++)
			{
				var random_generator = RandomNumberGenerator.new();
				random_generator.randomize();
				var random_value = random_generator.randf_range(0,Enemies.Count);
				
				fight.EnemyList[y] = Enemies[random_value];
			}
			level.FightList[y] = fight;
		}
		return level;
	}
}

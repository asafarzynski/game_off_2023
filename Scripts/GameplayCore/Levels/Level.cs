using Godot;

namespace GameOff2023.Scripts.GameplayCore.Levels;

public class Level
{
	public int LevelNumber;
	public Fight[] FightList;
	const int NUMBER_OF_FIGHTS = 4;

	public Level() : this(0, new Fight[NUMBER_OF_FIGHTS]) { }

	public Level(int levelNumber, Fight[] fightList)
	{
		LevelNumber = levelNumber;
		FightList = fightList;
	}
}

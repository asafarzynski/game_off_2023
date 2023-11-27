namespace GameOff2023.Scripts.GameplayCore.Levels;

public class Level
{
    private const int NUMBER_OF_FIGHTS = 4;

    public bool IsCleared
    {
        get
        {
            foreach (var fight in FightList)
            {
                if (fight.FightStatus != FightStatus.Win)
                    return false;
            }
            return true;
        }
    }
    
    public int CurrentFightIndex { get; internal set; }

    public readonly int LevelNumber;
    public readonly Fight[] FightList;

    public Level(int levelNumber) : this(levelNumber, new Fight[NUMBER_OF_FIGHTS]) { }

    public Level(int levelNumber, Fight[] fightList)
    {
        LevelNumber = levelNumber;
        FightList = fightList;
    }
}

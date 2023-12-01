using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class GoToNextBattleCommand : GameplayCoreCommand
{
    private Level _generatedLevel;
    
    public GoToNextBattleCommand(GameplayCore gameplayCore) : base(gameplayCore) { }
    
    public override CommandValidation Validate()
    {
        if (Core.LevelManager.CurrentFight.FightStatus == FightStatus.InProgress) // it does not really make sense in the current implementation but whatever
            return CommandValidationCreator.Invalid("Fight is still ongoing");

        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        if (Core.LevelManager.CurrentLevel.CurrentFightIndex == Core.LevelManager.CurrentLevel.FightList.Length - 1)
        {
            if (_generatedLevel == null)
            {
                _generatedLevel = Core.LevelManager.GenerateRandomLevel();
            }
            else
            {
                Core.LevelManager.Levels.Add(_generatedLevel); // in case of execute after unexecute (to not trigger random again, because results would be different)
            }
            Core.LevelManager.CurrentLevel.CurrentFightIndex = 0;
            Core.Events.OnLevelChanged?.Invoke();
        }
        else
        {
            Core.LevelManager.CurrentLevel.CurrentFightIndex++;
            Core.Events.OnNewBattleAhead?.Invoke();
        }
    }

    public override void UnExecute()
    {
        if (Core.LevelManager.CurrentLevel.CurrentFightIndex == 0)
        {
            Core.LevelManager.Levels.RemoveAt(Core.LevelManager.Levels.Count - 1);
            Core.LevelManager.CurrentLevel.CurrentFightIndex = Core.LevelManager.CurrentLevel.FightList.Length - 1;
        }
        else
        {
            Core.LevelManager.CurrentLevel.CurrentFightIndex--;
        }
    }
}

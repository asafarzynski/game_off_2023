using GameOff2023.Scripts.Commands;
namespace GameOff2023.Scripts.GameplayCore.Commands;

public class GoToNextLevelCommand : GameplayCoreCommand
{
    public GoToNextLevelCommand(GameplayCore gameplayCore) : base(gameplayCore) { }
    public override CommandValidation Validate()
    {
        if (!Core.LevelManager.CurrentLevel.IsCleared)
            return CommandValidationCreator.Invalid("Level not cleared yet!");

        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        Core.LevelManager.GenerateNextLevel();
        Core.Events.OnLevelChanged?.Invoke();
    }

    public override void UnExecute()
    {
        Core.LevelManager.Levels.RemoveAt(Core.LevelManager.Levels.Count - 1);
    }
}

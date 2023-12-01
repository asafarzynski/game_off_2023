using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class GoToBossBattleCommand : GameplayCoreCommand
{
    private Level _generatedLevel;
    public GoToBossBattleCommand(GameplayCore gameplayCore) : base(gameplayCore) { }

    public override CommandValidation Validate()
    {
        if (!Core.LevelManager.CurrentLevel.IsCleared) // we should only go to the boss room when we finished the level
            return CommandValidationCreator.Invalid("Fight is still ongoing");

        if (Core.LevelManager.CurrentLevel.LevelNumber < LevelManager.MIN_BOSS_LEVEL)
            return CommandValidationCreator.Invalid($"Current level is too low. You need to pass at least {LevelManager.MIN_BOSS_LEVEL} levels");

        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        if (_generatedLevel == null)
        {
            _generatedLevel = Core.LevelManager.AddSpecificLevel(Core.FinalBosses);
        }
        else
        {
            Core.LevelManager.Levels.Add(_generatedLevel); // in case of execute after unexecute (to not trigger random again, because results would be different)
        }
        Core.Events.OnLevelChanged?.Invoke();
        Core.Events.OnFinalBossesReached?.Invoke();
    }

    public override void UnExecute()
    {
        if (Core.LevelManager.CurrentLevel.CurrentFightIndex == 0)
        {
            Core.LevelManager.Levels.RemoveAt(Core.LevelManager.Levels.Count - 1);
        }
        else
        {
            Core.LevelManager.CurrentLevel.CurrentFightIndex--;
        }
    }
}

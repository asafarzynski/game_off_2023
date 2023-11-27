using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class FinishBattleCommand : GameplayCoreCommand
{
    private static bool _spellOrModifier;

    public FinishBattleCommand(GameplayCore gameplayCore) : base(gameplayCore) { }

    public override CommandValidation Validate()
    {
        if (Core.LevelManager.CurrentFight.FightStatus == FightStatus.InProgress) // it does not really make sense in the current implementation but whatever
            return CommandValidationCreator.Invalid("Fight is still ongoing");

        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        if (Core.LevelManager.CurrentFight.FightStatus == FightStatus.Win)
        {
            Core.CommandsExecutioner.Do(
                _spellOrModifier ? new GivePlayerRandomSpellModifierCommand(Core) : new GivePlayerRandomSpellCommand(Core));
            _spellOrModifier = !_spellOrModifier;
            Core.Events.OnBattleEnded?.Invoke();
        }
        else
        {
            Core.Events.OnGameOver?.Invoke();
        }
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

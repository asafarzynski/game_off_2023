using GameOff2023.Scripts.Commands;
namespace GameOff2023.Scripts.GameplayCore.Commands;

public class FightNextBattleCommand : GameplayCoreCommand
{
    public FightNextBattleCommand(GameplayCore gameplayCore) : base(gameplayCore) { }
    
    public override CommandValidation Validate()
    {
        var atLeastOneSpell = false;
        foreach (var slot in Core.Inventory.SpellSlots)
        {
            if (slot.Spell.HasValue)
            {
                atLeastOneSpell = true;
                break;
            }
        }
        if (!atLeastOneSpell)
        {
            return CommandValidationCreator.Invalid("No spells selected");
        }

        if (Core.LevelManager.CurrentLevel.IsCleared)
        {
            return CommandValidationCreator.Invalid("All fights on this level cleared");
        }
        
        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        Core.Events.OnBattleStarted?.Invoke();

        var currentLevel = Core.LevelManager.CurrentLevel;
        var currentFight = currentLevel.FightList[currentLevel.CurrentFightIndex];

        Core.SpellStack.Clear();
        
        // TODO: use spell stack to process battle
        currentFight.IsCleared = true;

        if (currentLevel.IsCleared)
        {
            Core.Events.OnLevelCleared?.Invoke();
        }
        else
        {
            currentLevel.CurrentFightIndex++;
        }
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

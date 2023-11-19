using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.Fight;
using Godot;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;
using GameOff2023.Scripts.GameStateManagement;
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


        currentFight.FightEvents = FightSimulator.Simulate();
        
        foreach (var fightEvent in currentFight.FightEvents)
        {
            FightSimulator.LogFightEvent(fightEvent);
        }

        // if(fightEvents[^1].FightResult == FightStatus.Win) {
        //     return true;
        // } else if(fightEvents[^1].FightResult == FightStatus.Draw) {
        //     return false;
        // } else {
        //     return false;
        // }

        // if(result)
        // {
        //     currentFight.IsCleared = true;
        // }
        // else {
        //     currentFight.IsCleared = false;
        // }

        // if (currentLevel.IsCleared)
        // {
        //     Core.Events.OnLevelCleared?.Invoke();
        // }
        // else
        // {
        //     currentLevel.CurrentFightIndex++;
        // }
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

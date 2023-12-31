using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.Fight;
using GameOff2023.Scripts.GameplayCore.Levels;

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

        Core.PlayerCharacter.Character.Spells = Core.Inventory.ParseSpells(); // we use spells selected from inventory

        var currentLevel = Core.LevelManager.CurrentLevel;
        var currentFight = currentLevel.FightList[currentLevel.CurrentFightIndex];

        currentFight.FightStatus = FightStatus.InProgress;

        currentFight.FightEvents = FightSimulator.SimulateFight(new FightingCharacter[]
            {
                Core.PlayerCharacter,
            },
            currentFight.EnemyList);

        currentFight.FightStatus = currentFight.FightEvents[^1].FightResult;
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

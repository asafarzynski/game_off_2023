using System;
using GameOff2023.Scripts.Commands;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class GivePlayerRandomSpellModifierCommand : GameplayCoreCommand
{
    private static Random _random = new();

    public GivePlayerRandomSpellModifierCommand(GameplayCore gameplayCore) : base(gameplayCore) { }

    public override CommandValidation Validate()
    {
        return CommandValidationCreator.Valid();
    }

    public override void Execute()
    {
        var randomIndex = _random.Next(Core.AllSpellModifiersInGame.Length);
        Core.Inventory.AddNewItem(Core.AllSpellModifiersInGame[randomIndex]);
        Core.Events.OnInventoryChanged?.Invoke();
    }

    public override void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}

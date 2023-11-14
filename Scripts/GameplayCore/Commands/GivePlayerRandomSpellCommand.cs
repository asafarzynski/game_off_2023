using System;
using GameOff2023.Scripts.Commands;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class GivePlayerRandomSpellCommand : ICommand
{
    private readonly GameplayCore _gameplayCore;

    private static Random _random = new();

    public GivePlayerRandomSpellCommand(GameplayCore gameplayCore)
    {
        _gameplayCore = gameplayCore;
    }
    
    public bool Validate()
    {
        return true;
    }

    public void Execute()
    {
        var randomIndex = _random.Next(_gameplayCore.AllSpellsInGame.Length);
        _gameplayCore.Inventory.AddNewItem(_gameplayCore.AllSpellsInGame[randomIndex]);
        _gameplayCore.Events.InventoryChanged?.Invoke();
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
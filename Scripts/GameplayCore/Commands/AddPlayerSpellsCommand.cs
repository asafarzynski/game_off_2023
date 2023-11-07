using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class AddPlayerSpellsCommand : ICommand
{
    private readonly GameplayCore _gameplayCore;

    private float _previousValue;

    public AddPlayerSpellsCommand(GameplayCore gameplayCore)
    {
        _gameplayCore = gameplayCore;
    }

    public bool Validate()
    {
        return true; // nothing to be validated
    }

    public void Execute()
    {
        GD.Print("Add two spells");
        _gameplayCore.SpellsStack.Add(new Spell());
        _gameplayCore.SpellsStack.Add(new Spell());
    }

    public void UnExecute()
    {
        // TODO: Not sure how to implement it in the easiest way yet.
    }
}
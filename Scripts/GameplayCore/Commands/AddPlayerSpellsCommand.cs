using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;
using System.Collections.Generic;

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
        var spellToAdd = new Spell();
        var spellToAddDifferentCooldown = new Spell(2, 1, 69, 0);
        AddSpell(spellToAdd);
        AddSpell(spellToAddDifferentCooldown);
    }

    private void AddSpell(Spell spell) {
        var spellCooldown = spell.Cooldown;
        if (_gameplayCore.SpellsStack.ContainsKey(spellCooldown))
        {
            _gameplayCore.SpellsStack[spellCooldown].Add(spell);
        }
        else
        {
            _gameplayCore.SpellsStack[spellCooldown] = new List<Spell> { spell };
        }
    }

    public void UnExecute()
    {
        // TODO: Not sure how to implement it in the easiest way yet.
    }
}

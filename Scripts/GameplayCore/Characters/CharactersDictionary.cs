using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore.Characters;

/// <summary>
/// Used for storing references to characters and nothing more.
/// </summary>
public class CharactersDictionary
{
    private Dictionary<ID<FightingCharacter, int>, FightingCharacter> _characters = new();

    internal void RegisterCharacter(FightingCharacter character)
    {
        _characters.Add(character.Id, character);
    }

    public FightingCharacter GetCharacter(ID<FightingCharacter, int> id)
    {
        return _characters[id];
    }
}

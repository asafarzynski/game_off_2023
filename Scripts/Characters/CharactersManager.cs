using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.Resources;
using Godot;

namespace GameOff2023.Scripts.Characters;

public partial class CharactersManager : Node3D
{
    [Export] private Node3D[] _playerPositions;
    [Export] private Node3D[] _enemyPositions;

    private readonly Dictionary<ID<FightingCharacter, int>, CharacterVisuals> _spawnedNodes = new();

    public void SpawnAllCharacters(FightingCharacter[] players, FightingCharacter[] enemies)
    {
        SpawnCharacters(players, _playerPositions);
        SpawnCharacters(enemies, _enemyPositions);
    }

    public void Clear()
    {
        foreach (var spawnedNode in _spawnedNodes.Values)
        {
            spawnedNode.QueueFree();
        }
        _spawnedNodes.Clear();
    }

    private void SpawnCharacters(FightingCharacter[] characters, Node3D[] positions)
    {
        for (var index = 0; index < characters.Length; index++)
        {
            var character = characters[index];
            var resource = ResourcesManager.GetResource<CharacterResource>(character.Character.ResourceId);
            var instantiated = resource.VisualsToSpawn.Instantiate<CharacterVisuals>();
            positions[index].AddChild(instantiated);
            _spawnedNodes.Add(character.Id, instantiated);
        }
    }
}

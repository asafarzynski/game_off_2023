using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.Resources;
using Godot;

namespace GameOff2023.Scripts.Characters;

public partial class CharactersManager : Node3D
{
    [Export] private float _charactersSpeed = 2f;
    [Export] private Node3D _playerSpawningPosition;
    [Export] private Node3D[] _playerPositions;
    [Export] private Node3D _enemySpawningPosition;
    [Export] private Node3D[] _enemyPositions;

    public event Action OnEverybodyInPosition;

    private readonly Dictionary<ID<FightingCharacter, int>, CharacterVisuals> _spawnedCharacters = new();
    private readonly Dictionary<ID<FightingCharacter, int>, Vector3> _desiredPositions = new();

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_desiredPositions.Count > 0)
        {
            var idsToRemove = new List<ID<FightingCharacter, int>>();
            foreach (var keyValuePair in _desiredPositions)
            {
                var spawnedCharacter = _spawnedCharacters[keyValuePair.Key];
                if (spawnedCharacter.Position.DistanceTo(keyValuePair.Value) <= 0.01f)
                {
                    idsToRemove.Add(keyValuePair.Key);
                    spawnedCharacter.AnimateWalk(false);
                }
                else
                {
                    spawnedCharacter.Position = spawnedCharacter.Position.Lerp(keyValuePair.Value, (float)delta * _charactersSpeed);
                }
            }
            foreach (var id in idsToRemove)
            {
                _desiredPositions.Remove(id);
            }

            if (_desiredPositions.Count == 0)
            {
                OnEverybodyInPosition?.Invoke();
            }
        }
    }

    public void ChangeAnimationSpeed(float newValue)
    {
        foreach (var visuals in _spawnedCharacters.Values)
        {
            visuals.SetAnimationSpeed(newValue);
        }
    }

    public void AnimateAttack(ID<FightingCharacter, int> attacker)
    {
        if (!_spawnedCharacters.TryGetValue(attacker, out var attackerVisuals))
            return;

        attackerVisuals.AnimateAttack();
    }

    public void AnimateHurt(ID<FightingCharacter, int> victim)
    {
        if (!_spawnedCharacters.TryGetValue(victim, out var victimVisuals))
            return;

        victimVisuals.AnimateHurt();
    }

    public void AnimateDeath(ID<FightingCharacter, int> targetCharacterId)
    {
        if (!_spawnedCharacters.TryGetValue(targetCharacterId, out var victimVisuals))
            return;

        victimVisuals.AnimateDeath();
    }

    public void SpawnAllCharacters(FightingCharacter[] players, FightingCharacter[] enemies)
    {
        SpawnCharacters(players, _playerSpawningPosition, _playerPositions);
        SpawnCharacters(enemies, _enemySpawningPosition, _enemyPositions);
    }

    public void Clear()
    {
        foreach (var spawnedNode in _spawnedCharacters.Values)
        {
            spawnedNode.QueueFree();
        }
        _spawnedCharacters.Clear();
        _desiredPositions.Clear();
    }

    private void SpawnCharacters(FightingCharacter[] characters, Node3D spawningPoint, Node3D[] positions)
    {
        for (var index = 0; index < characters.Length; index++)
        {
            SpawnCharacter(characters[index], spawningPoint.Position, positions[index].Position);
        }
    }

    private void SpawnCharacter(FightingCharacter character, Vector3 position, Vector3 desiredPosition)
    {
        var resource = ResourcesManager.GetResource<CharacterResource>(character.Character.ResourceId);
        var instantiated = resource.VisualsToSpawn.Instantiate<CharacterVisuals>();
        AddChild(instantiated);
        instantiated.Position = position;
        _spawnedCharacters.Add(character.Id, instantiated);
        _desiredPositions.Add(character.Id, desiredPosition);
        instantiated.AnimateWalk(true);
    }

    public void AnimateProgress()
    {
        foreach (var kvp in _spawnedCharacters)
        {
            _desiredPositions.TryAdd(kvp.Key, _enemySpawningPosition.Position);
        }
    }
}

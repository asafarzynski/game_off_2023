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
    private Node3D _playerSpawningPosition;
    private Node3D[] _playerPositions;
    private Node3D _enemySpawningPosition;
    private Node3D[] _enemyPositions;

    public event Action OnEverybodyInPosition;

    public readonly Dictionary<ID<FightingCharacter, int>, CharacterVisuals> SpawnedCharacters = new();
    private readonly Dictionary<ID<FightingCharacter, int>, Vector3> _desiredPositions = new();

    public override void _Ready()
    {
        base._Ready();
        _playerSpawningPosition = GetNode<Node3D>("%PlayerSpawnPosition");
        _enemySpawningPosition = GetNode<Node3D>("%EnemySpawnPosition");
        
        var playerPositionNodes = GetNode("%PlayerPositions").GetChildren();
        _playerPositions = new Node3D[playerPositionNodes.Count];
        for (var i = 0; i < playerPositionNodes.Count; i++)
        {
            _playerPositions[i] = (Node3D)playerPositionNodes[i];
        }
        
        var enemyPositionNodes = GetNode("%EnemyPositions").GetChildren();
        _enemyPositions = new Node3D[enemyPositionNodes.Count];
        for (var i = 0; i < enemyPositionNodes.Count; i++)
        {
            _enemyPositions[i] = (Node3D)enemyPositionNodes[i];
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_desiredPositions.Count > 0)
        {
            var idsToRemove = new List<ID<FightingCharacter, int>>();
            foreach (var keyValuePair in _desiredPositions)
            {
                var spawnedCharacter = SpawnedCharacters[keyValuePair.Key];
                if (spawnedCharacter.GlobalPosition.DistanceTo(keyValuePair.Value) <= 0.01f)
                {
                    idsToRemove.Add(keyValuePair.Key);
                    spawnedCharacter.AnimateWalk(false);
                }
                else
                {
                    spawnedCharacter.GlobalPosition = spawnedCharacter.GlobalPosition.Lerp(keyValuePair.Value, (float)delta * _charactersSpeed);
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
        foreach (var visuals in SpawnedCharacters.Values)
        {
            visuals.SetAnimationSpeed(newValue);
        }
    }

    public void AnimateAttack(ID<FightingCharacter, int> attacker)
    {
        if (!SpawnedCharacters.TryGetValue(attacker, out var attackerVisuals))
            return;

        attackerVisuals.AnimateAttack();
    }

    public void AnimateHurt(ID<FightingCharacter, int> victim)
    {
        if (!SpawnedCharacters.TryGetValue(victim, out var victimVisuals))
            return;

        victimVisuals.AnimateHurt();
    }

    public void AnimateDeath(ID<FightingCharacter, int> targetCharacterId)
    {
        if (!SpawnedCharacters.TryGetValue(targetCharacterId, out var victimVisuals))
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
        foreach (var spawnedNode in SpawnedCharacters.Values)
        {
            spawnedNode.QueueFree();
        }
        SpawnedCharacters.Clear();
        _desiredPositions.Clear();
    }

    private void SpawnCharacters(FightingCharacter[] characters, Node3D spawningPoint, Node3D[] positions)
    {
        for (var index = 0; index < characters.Length; index++)
        {
            SpawnCharacter(characters[index], spawningPoint.GlobalPosition, positions[index].GlobalPosition);
        }
    }

    private void SpawnCharacter(FightingCharacter character, Vector3 position, Vector3 desiredPosition)
    {
        var resource = ResourcesManager.GetResource<CharacterResource>(character.Character.ResourceId);
        var instantiated = resource.VisualsToSpawn.Instantiate<CharacterVisuals>();
        AddChild(instantiated);
        instantiated.GlobalPosition = position;
        SpawnedCharacters.Add(character.Id, instantiated);
        _desiredPositions.Add(character.Id, desiredPosition);
        instantiated.AnimateWalk(true);
    }

    public void AnimateProgress()
    {
        foreach (var kvp in SpawnedCharacters)
        {
            _desiredPositions.TryAdd(kvp.Key, _enemySpawningPosition.GlobalPosition);
        }
    }
}

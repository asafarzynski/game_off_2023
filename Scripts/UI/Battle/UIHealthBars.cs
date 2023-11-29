using System.Collections.Generic;
using System.Runtime;
using GameOff2023.Scripts.Characters;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.Resources;
using Godot;

namespace GameOff2023.Scripts.UI.Battle;

public partial class UIHealthBars : Control
{
    [Export] private Vector2 _healthBarOffset;

    private UIHealthBar _healthBarExample;

    private readonly Dictionary<ID<FightingCharacter, int>, UIHealthBar> _healthBars = new();
    private GameplayCore.GameplayCore _core;
    private Camera3D _camera;
    private CharactersManager _charactersManager;

    public override void _Ready()
    {
        base._Ready();
        _healthBarExample = GetNode<UIHealthBar>("HealthBar");
        _healthBarExample.Visible = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        foreach (var kvp in _healthBars)
        {
            kvp.Value.GlobalPosition = _camera.UnprojectPosition(_charactersManager.SpawnedCharacters[kvp.Key].GlobalPosition) + _healthBarOffset;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Clear();
    }

    public void SetUp(CharactersManager charactersManager, GameplayCore.GameplayCore core, Camera3D camera)
    {
        _core = core;
        _camera = camera;
        _charactersManager = charactersManager;

        foreach (var spawnedCharacter in charactersManager.SpawnedCharacters)
        {
            var newHealthBar = (UIHealthBar)_healthBarExample.Duplicate();

            var fightingCharacter = core.CharactersDictionary.GetCharacter(spawnedCharacter.Key);
            newHealthBar.SetLabel(fightingCharacter.Character.Name);
            newHealthBar.UpdateValue(Mathf.RoundToInt(fightingCharacter.FightStatus.Health / fightingCharacter.Character.Stats.Health * 100));

            _healthBars.Add(spawnedCharacter.Key, newHealthBar);
            AddChild(newHealthBar);
            newHealthBar.Visible = true;
        }
    }

    public void UpdateHealth(FightEvent fightEvent)
    {
        var targetHealth = Mathf.RoundToInt(fightEvent.TargetCharacterStatsAfter.Health / fightEvent.TargetCharacter.Character.Stats.Health * 100);
        if(targetHealth <= 0) {
            _healthBars[fightEvent.TargetCharacter.Id].Hide();
        } else {
            _healthBars[fightEvent.TargetCharacter.Id].UpdateValue(targetHealth);
        }
    }

    private void Clear()
    {
        foreach (var healthBar in _healthBars.Values)
        {
            healthBar.QueueFree();
        }
        _healthBars.Clear();
    }
}

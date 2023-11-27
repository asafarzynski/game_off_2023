using System;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class PreBattleSubState : UIManagingSubState<GameplayState>
{
    private readonly Func<LevelVisuals.LevelVisuals> _visualsGetter;

    public PreBattleSubState(Node uiParent, GameplayState id, Func<LevelVisuals.LevelVisuals> visualsGetter)
        : base(uiParent, id)
    {
        _visualsGetter = visualsGetter;
    }

    protected override string UIFilePath => "res://Scenes/UI/ui_pre_battle.tscn";

    internal override void Enter()
    {
        base.Enter();

        var levelVisuals = _visualsGetter();
        var core = GlobalGameData.Instance.Core;
        levelVisuals.CharactersManager.SpawnAllCharacters(new[] { core.PlayerCharacter }, core.LevelManager.CurrentFight.EnemyList);
    }
}

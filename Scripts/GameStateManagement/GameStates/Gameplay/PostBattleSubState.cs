using System;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class PostBattleSubState : UIManagingSubState<GameplayState>
{
    private readonly Func<LevelVisuals.LevelVisuals> _visualsGetter;

    public PostBattleSubState(Node uiParent, GameplayState id, Func<LevelVisuals.LevelVisuals> visualsGetter)
        : base(uiParent, id)
    {
        _visualsGetter = visualsGetter;
    }

    protected override string UIFilePath => "res://Scenes/UI/ui_post_battle.tscn";

    internal override void Enter()
    {
        base.Enter();

        var levelVisuals = _visualsGetter();
        levelVisuals.CharactersManager.Clear();
        
        // TODO: animate characters getting out of the scene instead
    }
}

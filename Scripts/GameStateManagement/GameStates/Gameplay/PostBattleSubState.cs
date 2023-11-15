using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class PostBattleSubState : UIManagingSubState<GameplayState>
{
    public PostBattleSubState(Node uiParent, GameplayState id)
        : base(uiParent, id) { }

    protected override string UIFilePath => "res://Scenes/UI/ui_post_battle.tscn";
}

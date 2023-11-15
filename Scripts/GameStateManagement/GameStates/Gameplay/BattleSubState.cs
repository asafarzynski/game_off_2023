using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class BattleSubState : UIManagingSubState<GameplayState>
{
    public BattleSubState(Node uiParent, GameplayState id)
        : base(uiParent, id) { }

    protected override string UIFilePath => "res://Scenes/UI/ui_battle.tscn";
}

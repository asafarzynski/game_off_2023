using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class PreBattleSubState : UIManagingSubState<GameplayState>
{
    public PreBattleSubState(Node uiParent, GameplayState id)
        : base(uiParent, id) { }

    protected override string UIFilePath => "res://Scenes/UI/ui_pre_battle.tscn";
}

using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class CreditsSubState : UIManagingSubState<MenuState>
{
    public CreditsSubState(Node uiParent, MenuState id) : base(uiParent, id)
    {
    }

    protected override string UIFilePath => "res://Scenes/UI/ui_credits.tscn";
}
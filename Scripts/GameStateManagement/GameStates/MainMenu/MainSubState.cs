using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class MainSubState : UIManagingSubState<MenuState>
{
    public MainSubState(Node uiParent, MenuState id) : base(uiParent, id)
    {
    }

    protected override string UIFilePath => "res://Scenes/UI/ui_main_menu.tscn";
}
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class MainSubState : MainMenuSubState
{
    public MainSubState(Node uiParent, MenuState id) : base(uiParent, id)
    {
    }

    protected override string UIFilePath => "res://Scenes/UI/MainMenu.tscn";
}
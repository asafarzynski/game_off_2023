using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class SettingsSubState : MainMenuSubState
{
    public SettingsSubState(Node uiParent, MenuState id) : base(uiParent, id)
    {
    }

    protected override string UIFilePath => "res://Scenes/UI/Settings.tscn";
}
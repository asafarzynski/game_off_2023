using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class SettingsSubState : UIManagingSubState<MenuState>
{
    public SettingsSubState(Node uiParent, MenuState id)
        : base(uiParent, id) { }

    protected override string UIFilePath => "res://Scenes/UI/ui_settings.tscn";
}

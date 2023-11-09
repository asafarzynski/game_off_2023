using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public class CreditsSubState : MainMenuSubState
{
    public CreditsSubState(Node uiParent, MenuState id) : base(uiParent, id)
    {
    }

    protected override string UIFilePath => "res://Scenes/UI/Credits.tscn";
}
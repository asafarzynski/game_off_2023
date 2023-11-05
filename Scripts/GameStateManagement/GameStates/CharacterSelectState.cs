using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class CharacterSelectState : GameState
{
    internal override string SceneName { get; } = "character_select";
    internal override void OnEnter()
    {
        GD.Print("entering character_select");
    }

    internal override void OnExit()
    {
        GD.Print("exiting character_select");
    }
}

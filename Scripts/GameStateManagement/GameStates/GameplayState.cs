using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class GameplayState : GameState
{
    internal override string SceneName { get; } = "gameplay";
    internal override void OnEnter()
    {
        GD.Print("entering gameplay");
    }

    internal override void OnExit()
    {
        GD.Print("exiting gameplay");
    }
}

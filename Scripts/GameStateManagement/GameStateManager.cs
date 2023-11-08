using GameOff2023.Scripts.Utils;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GameStateManager : NodeSingleton<GameStateManager>
{
    public GameState CurrentState { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // TODO: check current scene and set proper state
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ChangeState<T>() where T : GameState, new()
    {
        CurrentState?.OnExit();

        var newState = new T();
        var error = GetTree().ChangeSceneToFile($"res://Scenes/States/{newState.SceneName}.tscn");

        if (error == Error.Ok)
        {
            CurrentState = newState;
            CurrentState.OnEnter();
        }
        else
        {
            // TODO: handle errors
        }
    }
}
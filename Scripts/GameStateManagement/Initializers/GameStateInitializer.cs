using Godot;

namespace GameOff2023.Scripts.GameStateManagement.Initializers;

/// <summary>
/// It can be used to start a scene that is part of the game with specified initial state (defined in a derived class).
/// </summary>
public partial class GameStateInitializer : Node
{
    [Export]
    public GameStates.GameState GameState;

    public override void _Ready()
    {
        base._Ready();
        if (
            GameStateManager.Instance.StateMachine.TrySetInitialState(GameState, OnBeforeInitialSet)
        )
        {
            GD.Print($"Started state machine from {GameState}");
        }
    }

    protected virtual void OnBeforeInitialSet() { }
}

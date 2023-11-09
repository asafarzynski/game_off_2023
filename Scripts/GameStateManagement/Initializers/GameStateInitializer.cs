using Godot;

namespace GameOff2023.Scripts.GameStateManagement.Initializers;

public partial class GameStateInitializer : Node
{
    [Export] public GameStates.GameState GameState;

    public override void _Ready()
    {
        base._Ready();
        GD.Print($"Initializing {GameState}");
        if (GameStateManager.Instance.TryStartGameFromState(GameState))
        {
            GD.Print($"Started state machine from {GameState}");
            OnAfterInitialSet();
        }
    }
    
    protected virtual void OnAfterInitialSet() {}
}
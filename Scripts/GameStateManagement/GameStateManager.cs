using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.Utils;
using GameOff2023.Scripts.Utils.FSM;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GameStateManager : NodeSingleton<GameStateManager>
{
    public FSM<GameStates.GameState, Triggers, GameState> StateMachine { get; private set; }

    private FSMLogger<GameStates.GameState, Triggers, GameState> _fsmLogger;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateMachine = new();

        StateMachine.AddState(GameStates.GameState.Empty, null);
        var menuState = new MainMenuState(this, GameStates.GameState.MainMenu);
        StateMachine.AddState(menuState);
        var gameplayState = new GameplayState(this, GameStates.GameState.Gameplay);
        StateMachine.AddState(gameplayState);

        StateMachine.AddTransition(menuState.Id, gameplayState.Id, Triggers.GameStarted);
        StateMachine.AddTransition(gameplayState.Id, menuState.Id, Triggers.GameEnded);

        _fsmLogger = new(StateMachine, "Game State Machine");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        StateMachine.Update(delta);
    }
}

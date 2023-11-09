using System;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.Utils;
using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GameStateManager : NodeSingleton<GameStateManager>
{
    public FSM<GameStates.GameState, Triggers, GameState> StateMachine { get; private set; }

    private bool _skipLoadingScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateMachine = new ();

        StateMachine.AddState(GameStates.GameState.Empty, null);
        var menuState = new MainMenuState(GameStates.GameState.MainMenu);
        StateMachine.AddState(menuState);
        var gameplayState = new GameplayState(GameStates.GameState.Gameplay);
        StateMachine.AddState(gameplayState);
        
        StateMachine.AddTransition(menuState.Id,
            gameplayState.Id,
            Triggers.GameStarted);
        StateMachine.AddTransition(gameplayState.Id,
            menuState.Id,
            Triggers.GameEnded);

        StateMachine.OnEnter += LoadScene;
        StateMachine.OnTriggerFired += PrintTrigger;
        StateMachine.OnTransitionStarted += PrintTransition;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        StateMachine.Update(delta);
    }

    /// <summary>
    /// Use only in "initializers"
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool TryStartGameFromState(GameStates.GameState state)
    {
        return StateMachine.TrySetInitialState(state, () => _skipLoadingScene = true);
    }

    private void LoadScene(GameState fsmState)
    {
        GD.Print($"Entering {fsmState?.Id}");

        if (fsmState == null)
            return;
        
        if (_skipLoadingScene)
        {
            _skipLoadingScene = false;
            GD.Print("Skipping loading a scene");
            return;
        }

        var error = GetTree().ChangeSceneToFile($"res://Scenes/States/{fsmState.SceneName}.tscn");
        
        if (error != Error.Ok)
        {
            throw new Exception($"Error when loading scene file linked to the {fsmState.Id} state:\n{error}");
        }
    }

    private void PrintTrigger(Triggers trigger)
    {
        GD.Print($"Trigger fired: {trigger}");
    }

    private void PrintTransition(FSMTransition<GameStates.GameState, Triggers> fsmTransition)
    {
        GD.Print($"Transitioning from {fsmTransition.FromState} to {fsmTransition.ToState} because of {fsmTransition.Trigger}");
    }
}
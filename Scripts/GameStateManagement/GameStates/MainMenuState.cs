using System;
using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;
using GameOff2023.Scripts.UI;
using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class MainMenuState : GameStateManagement.GameState
{
    internal override string SceneName { get; } = "main_menu";

    public FSM<MenuState, MenuTrigger, UIManagingSubState<MenuState>> InnerStateMachine { get; private set; }

    private FSMLogger<MenuState, MenuTrigger, UIManagingSubState<MenuState>> _fsmLogger;

    public MainMenuState(Node parentNode, GameState id) : base(parentNode, id)
    {
        InnerStateMachine = new();

        InnerStateMachine.AddState(MenuState.Empty, null);
        var mainState = new MainSubState(UIManager.Instance, MenuState.Menu);
        InnerStateMachine.AddState(mainState);
        var creditsState = new CreditsSubState(UIManager.Instance, MenuState.Credits);
        InnerStateMachine.AddState(creditsState);
        var settingsState = new SettingsSubState(UIManager.Instance, MenuState.Settings);
        InnerStateMachine.AddState(settingsState);

        InnerStateMachine.AddTransition(mainState.Id, creditsState.Id, MenuTrigger.CreditsOpened);
        InnerStateMachine.AddTransition(creditsState.Id, mainState.Id, MenuTrigger.CreditsClosed);

        InnerStateMachine.AddTransition(mainState.Id, settingsState.Id, MenuTrigger.SettingsOpened);
        InnerStateMachine.AddTransition(settingsState.Id, mainState.Id, MenuTrigger.SettingsClosed);

        _fsmLogger = new(InnerStateMachine, "Main Menu Sub-State Machine");
    }

    internal override void Enter()
    {
        base.Enter();

        if (!InnerStateMachine.TrySetInitialState(MenuState.Menu))
        {
            throw new Exception("Why the initial state cannot be set up?");
        }

        AudioManager.Instance.PlayMusic();
    }

    internal override void Update(double deltaTime)
    {
        base.Update(deltaTime);
        InnerStateMachine.Update(deltaTime);
    }

    internal override void Exit()
    {
        base.Exit();
        InnerStateMachine.ExitAllStates();
        
        AudioManager.Instance.StopMusic();
    }
}
using System;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameOff2023.Scripts.UI;
using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class GameplayState : GameStateManagement.GameState
{
    internal override string SceneName { get; } = "gameplay";

    public FSM<
        Gameplay.GameplayState,
        GameplayTrigger,
        UIManagingSubState<Gameplay.GameplayState>
    > InnerStateMachine { get; private set; }

    private LevelVisuals.LevelVisuals _levelVisuals;

    private FSMLogger<
        Gameplay.GameplayState,
        GameplayTrigger,
        UIManagingSubState<Gameplay.GameplayState>
    > _fsmLogger;

    public GameplayState(Node parentNode, GameState id)
        : base(parentNode, id)
    {
        InnerStateMachine = new();

        InnerStateMachine.AddState(Gameplay.GameplayState.Empty, null);
        var preBattle = new PreBattleSubState(UIManager.Instance, Gameplay.GameplayState.PreBattle, VisualsGetter);
        InnerStateMachine.AddState(preBattle);
        var battle = new BattleSubState(UIManager.Instance, Gameplay.GameplayState.Battle, VisualsGetter);
        InnerStateMachine.AddState(battle);
        var postBattle = new PostBattleSubState(UIManager.Instance, Gameplay.GameplayState.PostBattle, VisualsGetter);
        InnerStateMachine.AddState(postBattle);

        InnerStateMachine.AddTransition(preBattle.Id, battle.Id, GameplayTrigger.BattleStarted);
        InnerStateMachine.AddTransition(battle.Id, postBattle.Id, GameplayTrigger.BattleEnded);
        InnerStateMachine.AddTransition(
            postBattle.Id,
            preBattle.Id,
            GameplayTrigger.NextBattleRequested
        );

        _fsmLogger = new(InnerStateMachine, "Gameplay Sub-State Machine");
    }

    internal override void Enter()
    {
        base.Enter();

        _levelVisuals = (LevelVisuals.LevelVisuals)LoadedScene.FindChild("LevelVisuals");

        if (!InnerStateMachine.TrySetInitialState(Gameplay.GameplayState.PreBattle))
        {
            throw new Exception("Why the initial state cannot be set up?");
        }

        GlobalGameData.Instance.Core.Events.OnBattleStarted += MoveToBattle;
        GlobalGameData.Instance.Core.Events.OnBattleEnded += MoveToSummary;
        GlobalGameData.Instance.Core.Events.OnNewBattleAhead += MoveToPreparations;
        GlobalGameData.Instance.Core.Events.OnGameOver += MoveToSummary;

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

        GlobalGameData.Instance.Core.Events.OnBattleStarted -= MoveToBattle;
        GlobalGameData.Instance.Core.Events.OnBattleEnded -= MoveToSummary;
        GlobalGameData.Instance.Core.Events.OnNewBattleAhead -= MoveToPreparations;
        GlobalGameData.Instance.Core.Events.OnGameOver -= MoveToSummary;
        
        InnerStateMachine.ExitAllStates();

        AudioManager.Instance.StopMusic();
    }

    private LevelVisuals.LevelVisuals VisualsGetter() => _levelVisuals;
    private void MoveToBattle()
    {
        InnerStateMachine.Trigger(GameplayTrigger.BattleStarted);
    }

    private void MoveToSummary()
    {
        InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    private void MoveToPreparations()
    {
        InnerStateMachine.Trigger(GameplayTrigger.NextBattleRequested);
    }
}

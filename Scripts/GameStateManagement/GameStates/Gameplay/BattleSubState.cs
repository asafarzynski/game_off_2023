using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Levels;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class BattleSubState : UIManagingSubState<GameplayState>
{
    public event Action<FightEvent, bool> OnFightEventFired;
    public event Action OnFightEnded;
    
    private Timer _timer;

    private int _eventIndex;
    private int _lastCooldown;
    private float _timeSpeed = 1f;

    private List<FightEvent> FightEvents => GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;

    public BattleSubState(Node uiParent, GameplayState id)
        : base(uiParent, id)
    {}

    protected override string UIFilePath => "res://Scenes/UI/ui_battle.tscn";

    internal override void Enter()
    {
        base.Enter();

        _timer = ((GameStates.GameplayState)GameStateManager.Instance.StateMachine.CurrentState).LoadedScene.GetNode<Timer>("Timer");
        _timer.Timeout += TimeTick;
        _timer.Start();

        _eventIndex = 0;
    }

    internal override void Exit()
    {
        base.Exit();
        
        _timer.Timeout -= TimeTick;
    }

    public void ChangeSpeed(int speedValue)
    {
        var timeLeftFactor = _timeSpeed / speedValue;
        _timer.WaitTime = _timer.TimeLeft * timeLeftFactor;
        _timeSpeed = speedValue;
    }

    public void Pause(bool toggled)
    {
        _timer.Paused = toggled;
    }

    public void FastForward()
    {
        while (_eventIndex < FightEvents.Count)
        {
            TimeTick(true);
        }
    }

    private void TimeTick() => TimeTick(false);
    

    private void TimeTick(bool skip)
    {
        _timer.Stop();
        
        if (_eventIndex >= FightEvents.Count)
            return;
        
        var currentEvent = FightEvents[_eventIndex++];
        OnFightEventFired?.Invoke(currentEvent, skip);

        if (_eventIndex < FightEvents.Count)
        {
            var nextEvent = FightEvents[_eventIndex];
            int? nextWaitTime = nextEvent.SpellCast?.Cooldown - _lastCooldown;
            if (nextWaitTime is null or < 1)
            {
                // events happening in the same time (kinda)
                TimeTick(skip);
                return;
            }
            _timer.WaitTime = nextWaitTime.Value / _timeSpeed;
            _timer.Start();
        }
        else
        {
            OnFightEnded?.Invoke();
        }
        
        if (currentEvent.SpellCast != null)
        {
            _lastCooldown = currentEvent.SpellCast.Cooldown;
        }
    }
}

using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Levels;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class BattleSubState : UIManagingSubState<GameplayState>
{
    public event Action<FightEvent, bool> OnFightEventFired;
    public event Action OnFightEventsEnded;
    
    private Timer _timer;

    private int _eventIndex;
    private int _lastCooldown;
    private float _timeSpeed = 1f;
    
    private readonly Func<LevelVisuals.LevelVisuals> _visualsGetter;

    private List<FightEvent> FightEvents => GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;

    public BattleSubState(Node uiParent, GameplayState id, Func<LevelVisuals.LevelVisuals> visualsGetter)
        : base(uiParent, id)
    {
        _visualsGetter = visualsGetter;
    }

    protected override string UIFilePath => "res://Scenes/UI/ui_battle.tscn";

    internal override void Enter()
    {
        base.Enter();

        _eventIndex = 0;
        _lastCooldown = 0;

        _timer = ((GameStates.GameplayState)GameStateManager.Instance.StateMachine.CurrentState).LoadedScene.GetNode<Timer>("Timer");
        _timer.WaitTime = 1f;
        _timer.Timeout += TimeTick;
        _timer.Start();
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
        if (!skip)
        {
            AnimateEvent(currentEvent);
        }
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
        
            if (currentEvent.SpellCast != null)
            {
                _lastCooldown = currentEvent.SpellCast.Cooldown;
            }
        }
        else
        {
            OnFightEventsEnded?.Invoke();
        }
    }

    private void AnimateEvent(FightEvent fightEvent)
    {
        if (fightEvent.EventType == FightEventType.SpellCast)
        {
            _visualsGetter().CharactersManager.AnimateAttack(fightEvent.SpellCast.OriginCharacter.Id);
        }
    }
}

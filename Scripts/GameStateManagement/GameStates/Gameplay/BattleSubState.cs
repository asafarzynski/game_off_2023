using System;
using System.Collections.Generic;
using System.Numerics;
using GameOff2023.Scripts.Characters;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;
using Vector3 = Godot.Vector3;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;

public class BattleSubState : UIManagingSubState<GameplayState>
{
    public event Action<FightEvent, bool> OnFightEventFired;
    public event Action OnFightEventsEnded;
    
    private Timer _timer;

    private int _eventIndex;
    private int _lastCooldown;
    private float _timeSpeed = 1f;

    public readonly Func<LevelVisuals.LevelVisuals> VisualsGetter;

    private List<FightEvent> FightEvents => GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;

    public BattleSubState(Node uiParent, GameplayState id, Func<LevelVisuals.LevelVisuals> visualsGetter)
        : base(uiParent, id)
    {
        VisualsGetter = visualsGetter;
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

        VisualsGetter().CharactersManager.ChangeAnimationSpeed(_timeSpeed);
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
        AnimateEvent(currentEvent, skip);
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

    private void AnimateEvent(FightEvent fightEvent, bool skip)
    {
        if (!skip && fightEvent.EventType == FightEventType.SpellCast)
        {
            var visuals = VisualsGetter();
            var fromPosition = visuals.CharactersManager.SpawnedCharacters[fightEvent.SpellCast.OriginCharacter.Id].GlobalPosition;
            var toPosition = visuals.CharactersManager.SpawnedCharacters[fightEvent.TargetCharacter.Id].GlobalPosition + Vector3.Up;
            var targetCharacterVisuals = visuals.CharactersManager.SpawnedCharacters[fightEvent.TargetCharacter.Id];
            visuals.AnimateSpell(fightEvent.SpellCast.Spell, targetCharacterVisuals, fromPosition, toPosition);
            visuals.CharactersManager.AnimateAttack(fightEvent.SpellCast.OriginCharacter.Id);
        }
        if (fightEvent.EventType == FightEventType.CharacterDeath) // never skip death
        {
            var visuals = VisualsGetter();
            visuals.CharactersManager.AnimateDeath(fightEvent.TargetCharacter.Id);
        }
    }
}

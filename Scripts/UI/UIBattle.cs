using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;
using GameOff2023.Scripts.GameStateManagement;
using Godot;
using GameOff2023.Scripts.GameplayCore.Levels;
using System.Collections.Generic;
using GameOff2023.Scripts.UI.Battle;

namespace GameOff2023.Scripts.UI;

public partial class UIBattle : UIGameStateSpecific<GameplayState>
{
    // show combat UI here
    private Button _summaryButton;
    private Timer _timer;
    private RichTextLabel _fightLogText;

    private int _eventIndex;
    private int _lastCooldown;
    private float _timeSpeed = 1f;

    private List<FightEvent> FightEvents => GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;

    public override void _Ready()
    {
        base._Ready();
        _summaryButton = (Button)FindChild("SummaryButton");
        _summaryButton.Disabled = true;

        _fightLogText = GetNode<RichTextLabel>("FightLog/Text");
        _fightLogText.Text = "";

        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += TimeTick;
        _timer.Start();

        _eventIndex = 0;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        _timer.Timeout -= TimeTick;
    }

    public void _on_speed_toggled(bool _, int speedValue)
    {
        var timeLeftFactor = _timeSpeed / speedValue;
        _timer.WaitTime = _timer.TimeLeft * timeLeftFactor;
        _timeSpeed = speedValue;
    }

    public void _on_pause_toggled(bool toggled)
    {
        _timer.Paused = toggled;
    }

    public void _on_summary_button_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    public void _on_fast_forward_pressed()
    {
        foreach (var nextEvent in FightEvents)
        {
            LogFightEvent(nextEvent);
        }
        _eventIndex = FightEvents.Count;
        _summaryButton.Disabled = false;
    }

    private void LogFightEvent(FightEvent nextEvent)
    {
        var logEntry = FightLogger.LogFightEvent(nextEvent);
        _fightLogText.Text += logEntry + "\n";
    }

    private void TimeTick()
    {
        _timer.Stop();
        
        if (_eventIndex >= FightEvents.Count)
            return;
        
        var currentEvent = FightEvents[_eventIndex++];
        LogFightEvent(currentEvent);

        if (_eventIndex < FightEvents.Count)
        {
            var nextEvent = FightEvents[_eventIndex];
            int? nextWaitTime = nextEvent.SpellCast?.Cooldown - _lastCooldown;
            if (nextWaitTime is null or < 1)
            {
                // events happening in the same time (kinda)
                TimeTick();
                return;
            }
            _timer.WaitTime = nextWaitTime.Value / _timeSpeed;
            _timer.Start();
        }
        else
        {
            _summaryButton.Disabled = false;
        }
        if (currentEvent.SpellCast != null)
        {
            _lastCooldown = currentEvent.SpellCast.Cooldown;
        }
    }
}

using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;
using Godot;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.UI.Battle;

namespace GameOff2023.Scripts.UI;

public partial class UIBattle : UIGameStateSpecific<GameplayState>
{
    // show combat UI here
    private Button _summaryButton;
    private RichTextLabel _fightLogText;
    private UIHealthBars _healthBars;

    private BattleSubState _subState;

    public override void _Ready()
    {
        base._Ready();

        _subState = ((BattleSubState)State.InnerStateMachine.CurrentState);
        var visuals = _subState.VisualsGetter();
        
        _summaryButton = (Button)FindChild("SummaryButton");
        _summaryButton.Disabled = true;
        
        _healthBars = GetNode<UIHealthBars>("%HealthBars");
        _healthBars.SetUp(visuals.CharactersManager, GlobalGameData.Instance.Core, visuals.Camera);

        _fightLogText = GetNode<RichTextLabel>("FightLog/Text");
        _fightLogText.Text = "";

        _subState.OnFightEventFired += LogFightEvent;
        _subState.OnFightEventsEnded += UnlockSummaryButton;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        _subState.OnFightEventFired -= LogFightEvent;
        _subState.OnFightEventsEnded -= UnlockSummaryButton;
    }

    public void _on_speed_toggled(bool _, int speedValue)
    {
        _subState.ChangeSpeed(speedValue);
    }

    public void _on_pause_toggled(bool toggled)
    {
        _subState.Pause(toggled);
    }

    public void _on_summary_button_pressed()
    {
        GlobalGameData.Instance.Core.CommandsExecutioner.Do(new FinishBattleCommand(GlobalGameData.Instance.Core));
    }

    public void _on_fast_forward_pressed()
    {
        _subState.FastForward();
        _summaryButton.Disabled = false;
    }

    private void LogFightEvent(FightEvent nextEvent, bool _)
    {
        var logEntry = FightLogger.LogFightEvent(nextEvent);
        _fightLogText.Text += logEntry + "\n";

        if (nextEvent.TargetCharacter != null)
        {
            _healthBars.UpdateHealth(nextEvent);
        }
    }

    private void UnlockSummaryButton()
    {
        _summaryButton.Disabled = false;
    }
}

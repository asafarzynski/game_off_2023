using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.Fight;
using Godot;
using System.Reflection.Metadata.Ecma335;
using GameOff2023.Scripts.GameplayCore.Levels;
using System.Collections.Generic;


namespace GameOff2023.Scripts.UI;

public partial class UIBattle : UIGameStateSpecific<GameplayState>
{
    // show combat UI here

    private RichTextLabel _fightLogText;

    public override void _Ready()
    {
        base._Ready();
        _fightLogText = GetNode<RichTextLabel>("FightLog/Text");
        _fightLogText.Text = "";
    }

    public void _on_win_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    public void _on_lose_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    public void _on_next_turn_pressed()
    {
        var fightEvents = GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;
        var nextEvent = fightEvents[0];
        fightEvents.RemoveAt(0);
        LogFightEvent(nextEvent);
    }

    public void _on_fast_forward_pressed()
    {
        List<FightEvent> fightEvents = GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents;
        foreach(var nextEvent in fightEvents) {
            LogFightEvent(nextEvent);
        }
        fightEvents.Clear();
    }

    private void LogFightEvent(FightEvent nextEvent) {
        var logEntry = FightSimulator.LogFightEvent(nextEvent);
        _fightLogText.Text += logEntry + "\n"; 
    }
}

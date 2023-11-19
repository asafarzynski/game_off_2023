using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.Fight;
using Godot;


namespace GameOff2023.Scripts.UI;

public partial class UIBattle : UIGameStateSpecific<GameplayState>
{
    // show combat UI here

    public void _on_win_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    public void _on_lose_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleEnded);
    }

    public void _on_fight_pressed()
    {
        foreach (var fightEvent in GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightEvents)
        {
            FightSimulator.LogFightEvent(fightEvent);
        }
    }
}

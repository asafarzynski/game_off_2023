using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

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
}
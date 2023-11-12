using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPreBattle : UIGameStateSpecific<GameplayState>
{
    // show spell selection UI here

    public void _on_confirm_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleStarted);
    }

    public void _on_exit_pressed()
    {
        GameStateManager.Instance.StateMachine.Trigger(Triggers.GameEnded);
    }
}

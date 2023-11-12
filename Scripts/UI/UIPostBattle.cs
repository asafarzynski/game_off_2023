using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPostBattle : UIGameStateSpecific<GameplayState>
{
    public void _on_next_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.NextBattleRequested);
    }
}

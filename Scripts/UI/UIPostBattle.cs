using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPostBattle : UIGameStateSpecific<GameplayState>
{
    public void _on_give_pressed()
    {
        GlobalGameData.Instance.Core.CommandsExecutioner.Do(
            new GivePlayerRandomSpellCommand(GlobalGameData.Instance.Core));
    }

    public void _on_next_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.NextBattleRequested);
    }
}

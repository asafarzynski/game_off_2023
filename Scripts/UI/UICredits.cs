using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;
using Godot;

namespace GameOff2023.Scripts.UI;

public partial class UICredits : Control
{
    public void _on_back_pressed()
    {
        if (GameStateManager.Instance.StateMachine.CurrentState is MainMenuState mainMenuState) // oof, this is ugly
        {
            mainMenuState.InnerStateMachine.Trigger(MenuTrigger.CreditsClosed);
        }
    }
}
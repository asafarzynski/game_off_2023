using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

namespace GameOff2023.Scripts.UI;

public partial class UIMainMenu : UIGameStateSpecific<MainMenuState>
{
    public void _on_start_button_pressed()
    {
        GlobalGameData.Instance.SetUpNewCore();
        GameStateManager.Instance.StateMachine.Trigger(Triggers.GameStarted);
    }

    public void _on_credits_button_pressed()
    {
        State.InnerStateMachine.Trigger(MenuTrigger.CreditsOpened);
    }

    public void _on_settings_button_pressed()
    {
        State.InnerStateMachine.Trigger(MenuTrigger.SettingsOpened);
    }

    public void _on_exit_button_pressed()
    {
        GetTree().Quit();
    }
}
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

namespace GameOff2023.Scripts.UI;

public partial class UISettings : UIGameStateSpecific<MainMenuState>
{
    public void _on_back_pressed()
    {
        State.InnerStateMachine.Trigger(MenuTrigger.SettingsClosed);
    }
}

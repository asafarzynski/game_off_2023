using Godot;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

namespace GameOff2023.Scripts.UI;

public partial class UIMainMenu : Control
{
  public void _on_start_button_pressed()
  {
	GameStateManager.Instance.StateMachine.Trigger(Triggers.GameStarted);
  }

  public void _on_credits_button_pressed()
  {
	  if (GameStateManager.Instance.StateMachine.CurrentState is MainMenuState mainMenuState) // oof, this is ugly
	  {
		  mainMenuState.InnerStateMachine.Trigger(MenuTrigger.CreditsOpened);
	  }
  }

  public void _on_settings_button_pressed()
  {
	  if (GameStateManager.Instance.StateMachine.CurrentState is MainMenuState mainMenuState) // oof, this is ugly
	  {
		  mainMenuState.InnerStateMachine.Trigger(MenuTrigger.SettingsOpened);
	  }
  }

  public void _on_exit_button_pressed()
  {
	GetTree().Quit();
  }
}

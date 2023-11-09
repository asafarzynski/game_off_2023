using Godot;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;

namespace GameOff2023.Scripts.UI;

public partial class UIMainMenu : Control
{
  public void _on_start_button_pressed()
  {
	GameStateManager.Instance.StateMachine.Trigger(Triggers.GameStarted);
  }

  public void _on_exit_button_pressed()
  {
	GetTree().Quit();
  }
}

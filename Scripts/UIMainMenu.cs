using Godot;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;

namespace GameOff2023.Scripts;

public partial class UIMainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_start_button_pressed()
	{
		GameStateManager.Instance.ChangeState<GameplayState>();
	}

	public void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}
}

using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class MainMenuState : GameState
{
	internal override string SceneName { get; } = "main_menu";
	
	internal override void OnEnter()
	{
		GD.Print("entering main menu");
	}

	internal override void OnExit()
	{
		GD.Print("exiting main menu");
	}
}

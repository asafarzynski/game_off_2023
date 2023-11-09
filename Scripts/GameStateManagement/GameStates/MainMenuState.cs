namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class MainMenuState : GameStateManagement.GameState
{
	internal override string SceneName { get; } = "main_menu";

	public MainMenuState(GameState id) : base(id)
	{
	}
}

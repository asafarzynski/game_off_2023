namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class GameplayState : GameStateManagement.GameState
{
	internal override string SceneName { get; } = "gameplay";

	public GameplayState(GameState id) : base(id)
	{
	}
}

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class CharacterSelectState : GameStateManagement.GameState
{
	internal override string SceneName { get; } = "character_select";

	public CharacterSelectState(GameState id) : base(id)
	{
	}
}

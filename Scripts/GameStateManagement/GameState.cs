using GameOff2023.Scripts.Utils.FSM;

namespace GameOff2023.Scripts.GameStateManagement;

public abstract class GameState : FSMState<GameStates.GameState>
{
	internal abstract string SceneName { get; }

	protected GameState(GameStates.GameState id) : base(id)
	{
	}
}

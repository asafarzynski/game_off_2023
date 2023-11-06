namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class GameplayState : GameState
{
    internal override string SceneName { get; } = "gameplay";
    internal override void OnEnter()
    {
        GlobalGameData.Instance.SetUpNewCore();
    }

    internal override void OnExit()
    {
        GlobalGameData.Instance.ClearCore();
    }
}

namespace GameOff2023.Scripts.GameStateManagement;

public abstract class GameState
{
    internal abstract string SceneName { get; }
    internal abstract void OnEnter();
    internal abstract void OnExit();
}
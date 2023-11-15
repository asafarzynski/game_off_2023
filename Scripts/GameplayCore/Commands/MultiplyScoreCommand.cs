using GameOff2023.Scripts.Commands;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class MultiplyScoreCommand : ICommand
{
    private readonly GameplayCore _gameplayCore;
    private readonly float _multiplication;

    private float _previousValue;

    public MultiplyScoreCommand(GameplayCore gameplayCore, float multiplication)
    {
        _gameplayCore = gameplayCore;
        _multiplication = multiplication;
    }

    public bool Validate()
    {
        return true; // nothing to be validated
    }

    public void Execute()
    {
        _previousValue = _gameplayCore.Score;
        _gameplayCore.Score *= _multiplication;
    }

    public void UnExecute()
    {
        _gameplayCore.Score = _previousValue;
    }
}

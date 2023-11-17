using GameOff2023.Scripts.Commands;

namespace GameOff2023.Scripts.GameplayCore.Commands;

public class MultiplyScoreCommand : GameplayCoreCommand
{
    private readonly float _multiplication;

    private float _previousValue;

    public MultiplyScoreCommand(GameplayCore gameplayCore, float multiplication) : base (gameplayCore)
    {
        _multiplication = multiplication;
    }

    public override CommandValidation Validate()
    {
        return CommandValidationCreator.Valid(); // nothing to be validated
    }

    public override void Execute()
    {
        _previousValue = Core.Score;
        Core.Score *= _multiplication;
    }

    public override void UnExecute()
    {
        Core.Score = _previousValue;
    }
}

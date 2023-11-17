using GameOff2023.Scripts.Commands;
namespace GameOff2023.Scripts.GameplayCore.Commands;

public abstract class GameplayCoreCommand : ICommand
{
    protected readonly GameplayCore Core;
    
    protected GameplayCoreCommand(GameplayCore gameplayCore)
    {
        Core = gameplayCore;
    }
    
    public abstract CommandValidation Validate();

    public abstract void Execute();

    public abstract void UnExecute();
}

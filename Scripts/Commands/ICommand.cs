namespace GameOff2023.Scripts.Commands;

public interface ICommand
{
    public CommandValidation Validate();

    public void Execute();
    public void UnExecute();
}

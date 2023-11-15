namespace GameOff2023.Scripts.Commands;

public interface ICommand
{
    public bool Validate();

    public void Execute();
    public void UnExecute();
}

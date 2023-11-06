using System.Collections.Generic;

namespace GameOff2023.Scripts.Commands;

public class CommandsExecutioner
{
    public readonly Stack<ICommand> DoneStack = new();
    public readonly Stack<ICommand> UndoneStack = new();

    public bool Do(ICommand command)
    {
        if (!command.Validate())
            return false;
        
        command.Execute();
        DoneStack.Push(command);
        UndoneStack.Clear();
        return true;

    }

    public void Undo(int numberOfUndos = 1)
    {
        for (int i = numberOfUndos; i > 0; i--)
        {
            var command = DoneStack.Pop();
            command.UnExecute();
            UndoneStack.Push(command);
        }
    }

    public void Redo(int numberOfRedos = 1)
    {
        for (int i = numberOfRedos; i > 0; i--)
        {
            var command = UndoneStack.Pop();
            command.Execute();
            DoneStack.Push(command);
        }
    }
}
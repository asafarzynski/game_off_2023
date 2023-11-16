namespace GameOff2023.Scripts.Commands;

public readonly struct CommandValidation
{
    public readonly string Message;
    public readonly bool IsValid;

    public CommandValidation(bool isValid, string message = null)
    {
        IsValid = isValid;
        Message = message;
    }
}

public static class CommandValidationCreator
{
    public static CommandValidation Invalid(string message = "Command Invalid. No explanation provided.") => new CommandValidation(false, message);

    public static CommandValidation Valid() => new CommandValidation(true);
}

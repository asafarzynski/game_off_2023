namespace GameOff2023.Scripts.GameplayCore.Id;

public class SimpleIdGenerator<T>
{
    private int _nextId;

    public ID<T, int> GetNextId()
    {
        return new ID<T, int>(_nextId++);
    }
}
namespace GameOff2023.Scripts.Utils.FSM;

public class FSMState<T>
{
    public readonly T Id;

    public FSMState(T id)
    {
        Id = id;
    }
    
    internal virtual void Exit()
    {
    }

    internal virtual void Enter()
    {
    }

    internal virtual void Update(double deltaTime)
    {
    }
}
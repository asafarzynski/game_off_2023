namespace GameOff2023.Scripts.Utils.FSM;

public class FSMTransition<TState, TTrigger>
{
    public readonly TState FromState;
    public readonly TState ToState;
    public readonly TTrigger Trigger;

    public FSMTransition(TState from, TState to, TTrigger trigger)
    {
        FromState = from;
        ToState = to;
        Trigger = trigger;
    }
}

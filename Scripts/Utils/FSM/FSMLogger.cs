using Godot;

namespace GameOff2023.Scripts.Utils.FSM;

/// <summary>
/// Links automatically to the FSM events and prints proper messages
/// </summary>
/// <typeparam name="TStateId"></typeparam>
/// <typeparam name="TTrigger"></typeparam>
/// <typeparam name="TState"></typeparam>
public class FSMLogger<TStateId, TTrigger, TState>
    where TStateId : notnull
    where TTrigger : notnull
    where TState : FSMState<TStateId>
{
    private FSM<TStateId, TTrigger, TState> _fsm;
    private readonly string _identifier;

    public FSMLogger(FSM<TStateId, TTrigger, TState> fsm, string identifier = "FSMLogger")
    {
        _fsm = fsm;

        _fsm.OnEnter += PrintOnEnter;
        _fsm.OnExit += PrintOnExit;
        _fsm.OnTriggerFired += PrintOnTriggerFired;
        _fsm.OnTransitionStarted += PrintOnTransitionStarted;

        _identifier = identifier;
        
        GD.Print(PrependWithFsmName("Linked!"));
    }

    public void Dispose()
    {
        _fsm.OnEnter -= PrintOnEnter;
        _fsm.OnExit -= PrintOnExit;
        _fsm.OnTriggerFired -= PrintOnTriggerFired;
        _fsm.OnTransitionStarted -= PrintOnTransitionStarted;

        _fsm = null;
    }

    private void PrintOnTransitionStarted(FSMTransition<TStateId, TTrigger> fsmTransition)
    {
        GD.Print(PrependWithFsmName(
            $"Transitioning from {fsmTransition.FromState} to {fsmTransition.ToState} because of {fsmTransition.Trigger}"));
    }

    private void PrintOnTriggerFired(TTrigger trigger)
    {
        GD.Print(PrependWithFsmName($"Trigger fired: {trigger}"));
    }

    private void PrintOnEnter(TState state)
    {
        GD.Print(PrependWithFsmName(state == null ? "Entered non-existing state" : $"Entered {state.Id}"));
    }

    private void PrintOnExit(TState state)
    {
        GD.Print(PrependWithFsmName(state == null ? "Exiting non-existing state" : $"Exiting {state.Id}"));
    }

    private string PrependWithFsmName(string text)
    {
        return $"[{_identifier}]: {text}";
    }
}
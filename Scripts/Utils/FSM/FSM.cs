using System;
using System.Collections.Generic;

namespace GameOff2023.Scripts.Utils.FSM;

public class FSM<TStateId, TTrigger, TState>
	where TStateId : notnull
	where TTrigger : notnull
	where TState : FSMState<TStateId>
{
	public event Action<TState> OnEnter;
	public event Action<TState> OnExit;
	public event Action<TTrigger> OnTriggerFired;
	public event Action<FSMTransition<TStateId, TTrigger>> OnTransitionStarted;

	protected readonly Dictionary<TStateId, TState> States;
	protected readonly Dictionary<TTrigger, Dictionary<TStateId, FSMTransition<TStateId, TTrigger>>> Transitions;
	protected TState CurrentState;

	public FSM()
	{
		States = new();
		Transitions = new();
	}

	public void Update(double deltaTime)
	{
		CurrentState?.Update(deltaTime);
	}

	public void AddState(TStateId id, TState state)
	{
		States.Add(id, state);
	}

	public void AddState(TState state)
	{
		States.Add(state.Id, state);
	}

	public void AddTransition(TStateId from, TStateId to, TTrigger trigger)
	{
		if (!Transitions.TryGetValue(trigger, out var transitionsFrom))
		{
			transitionsFrom = new();
			Transitions.Add(trigger, transitionsFrom);
		}

		if (transitionsFrom.ContainsKey(from))
		{
			throw new ArgumentException($"Transition from {from} with {trigger} already defined!");
		}

		transitionsFrom.Add(from, new FSMTransition<TStateId, TTrigger>(from, to, trigger));
	}

	public void Trigger(TTrigger trigger)
	{
		OnTriggerFired?.Invoke(trigger);
		if (!Transitions.TryGetValue(trigger, out var transitionsFrom))
			return;

		if (CurrentState == null)
		{
			if (transitionsFrom.TryGetValue(default, out var transitionFromEmpty))
			{
				OnTransitionStarted?.Invoke(transitionFromEmpty);
				SetCurrentState(GetState(transitionFromEmpty.ToState));
			}
			return;
		}

		if (CurrentState == null || !transitionsFrom.TryGetValue(CurrentState.Id, out var properTransition))
			return;

		OnTransitionStarted?.Invoke(properTransition);
		SetCurrentState(GetState(properTransition.ToState));
	}

	public bool TrySetInitialState(TStateId stateId, Action beforeSet = null)
	{
		if (CurrentState != null)
			return false;
		
		beforeSet?.Invoke();
		SetCurrentState(GetState(stateId));
		return true;
	}

	protected void SetCurrentState(TState state)
	{
		OnExit?.Invoke(CurrentState);
		CurrentState?.Exit();

		CurrentState = state;

		CurrentState?.Enter();
		OnEnter?.Invoke(CurrentState);
	}

	protected TState GetState(TStateId key)
	{
		return States[key];
	}
}

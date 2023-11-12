using System;
using GameOff2023.Scripts.GameStateManagement;
using Godot;

namespace GameOff2023.Scripts.UI;

public abstract partial class UIGameStateSpecific<T> : Control
    where T : GameState
{
    protected T State { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        if (GameStateManager.Instance.StateMachine.CurrentState is not T castedState)
        {
            throw new Exception("This UI should be used only in its specified GameState!");
        }

        State = castedState;
    }
}

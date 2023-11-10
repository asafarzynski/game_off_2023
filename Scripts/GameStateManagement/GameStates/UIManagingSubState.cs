using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public abstract class UIManagingSubState<T> : FSMState<T>
{
    protected abstract string UIFilePath { get; }
    
    private readonly Node _uiParent;
    
    private Node _loadedUI;

    public UIManagingSubState(Node uiParent, T id) : base(id)
    {
        _uiParent = uiParent;
    }

    internal override void Enter()
    {
        base.Enter();
        _loadedUI = ResourceLoader.Load<PackedScene>(UIFilePath).Instantiate(); // TODO: move loading UI resources to UIManager ?
        _uiParent.AddChild(_loadedUI);
    }

    internal override void Exit()
    {
        base.Exit();
        _loadedUI.QueueFree();
    }
}
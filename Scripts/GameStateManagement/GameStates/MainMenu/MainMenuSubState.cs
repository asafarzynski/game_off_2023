using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;

public abstract class MainMenuSubState : FSMState<MenuState>
{
    protected abstract string UIFilePath { get; }
    
    private readonly Node _uiParent;
    
    private Node _loadedUI;

    public MainMenuSubState(Node uiParent, MenuState id) : base(id)
    {
        _uiParent = uiParent;
    }

    internal override void Enter()
    {
        base.Enter();
        _loadedUI = GD.Load<Node>(UIFilePath);
        _uiParent?.AddChild(_loadedUI);
    }

    internal override void Exit()
    {
        base.Exit();
        _loadedUI.QueueFree();
    }
}
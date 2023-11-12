using GameOff2023.Scripts.Utils.FSM;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public abstract class GameState : FSMState<GameStates.GameState>
{
    internal abstract string SceneName { get; }

    protected Node ParentNode { get; private set; }
    private Node _loadedScene;

    protected GameState(Node parentNode, GameStates.GameState id)
        : base(id)
    {
        ParentNode = parentNode;
    }

    internal override void Enter()
    {
        base.Enter();

        _loadedScene = ResourceLoader
            .Load<PackedScene>($"res://Scenes/States/{SceneName}.tscn")
            .Instantiate();
        ParentNode.AddChild(_loadedScene);
    }

    internal override void Exit()
    {
        base.Exit();

        _loadedScene.QueueFree();
    }
}

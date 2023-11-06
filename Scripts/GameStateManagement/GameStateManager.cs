using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GameStateManager : Node
{
  public static GameStateManager Instance { get; private set; }

	private GameState currentState;
	
	public GameStateManager()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Free();
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// TODO: check current scene and set proper state
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

  public void ChangeState<T>() where T : GameState, new()
  {
    currentState?.OnExit();

    var newScene = new T();
    var error = GetTree().ChangeSceneToFile($"res://Scenes/States/{newScene.SceneName}.tscn");

    if (error == Error.Ok)
    {
      currentState = newScene;
      currentState.OnEnter();
    }
    else
    {
      // TODO: handle errors
    }
  }
}

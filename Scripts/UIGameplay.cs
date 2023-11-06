using Godot;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;

namespace GameOff2023.Scripts.UI;

public partial class UIGameplay : Control
{
	private GameplayCore.GameplayCore _gameplayCore; // THE CORE SHOULD NOT BE HERE AS THIS CLASS SHOULD ONLY MANAGE UI STUFF (JUST TESTING NOW)
	private CommandsExecutioner _commandsExecutioner;

	private Label _scoreLabel;
	private Label _historyLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("%Score");
		_historyLabel = GetNode<Label>("%History");
		
		_gameplayCore = new GameplayCore.GameplayCore();
		_commandsExecutioner = new CommandsExecutioner();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_scoreLabel.Text = $"Score: {_gameplayCore.Score}";
		_historyLabel.Text = $"Done stack size: {_commandsExecutioner.DoneStack.Count}; Undone stack: {_commandsExecutioner.UndoneStack.Count}";
	}

	public void _on_button_pressed()
	{
		GameStateManager.Instance.ChangeState<MainMenuState>();
	}

	public void _on_multiply_pressed()
	{
		_commandsExecutioner.Do(new MultiplyScoreCommand(_gameplayCore, 2));
	}

	public void _on_undo_pressed()
	{
		_commandsExecutioner.Undo();
	}

	public void _on_redo_pressed()
	{
		_commandsExecutioner.Redo();
	}
}

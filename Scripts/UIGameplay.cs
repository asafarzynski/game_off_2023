using Godot;
using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;

namespace GameOff2023.Scripts.UI;

public partial class UIGameplay : Control
{
	private GameplayCore.GameplayCore _gameplayCore;

	private Label _scoreLabel;
	private Label _historyLabel;
	private Label _spellsLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("%Score");
		_historyLabel = GetNode<Label>("%History");
		_spellsLabel = GetNode<Label>("%Spells");

		_gameplayCore = GlobalGameData.Instance.Core;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_scoreLabel.Text = $"Score: {_gameplayCore.Score}";
		_historyLabel.Text = $"Done stack size: {_gameplayCore.CommandsExecutioner.DoneStack.Count}; Undone stack: {_gameplayCore.CommandsExecutioner.UndoneStack.Count}";
		_spellsLabel.Text = $"Available spells:\n{string.Join("\n", _gameplayCore.AvailableSpells)}";
	}

	public void _on_button_pressed()
	{
		GameStateManager.Instance.ChangeState<MainMenuState>();
	}

	public void _on_multiply_pressed()
	{
		_gameplayCore.CommandsExecutioner.Do(new MultiplyScoreCommand(_gameplayCore, 2));
	}

	public void _on_undo_pressed()
	{
		_gameplayCore.CommandsExecutioner.Undo();
	}

	public void _on_redo_pressed()
	{
		_gameplayCore.CommandsExecutioner.Redo();
	}
}

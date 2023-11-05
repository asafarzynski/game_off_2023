using Godot;
using System;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;

public partial class UIGameplay : Control
{
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  public void _on_button_pressed()
  {
    GameStateManager.Instance.ChangeState<MainMenuState>();
  }
}

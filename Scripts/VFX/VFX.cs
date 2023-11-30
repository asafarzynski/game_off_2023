using GameOff2023.Scripts.Characters;
using Godot;
using System;

public partial class VFX : Node3D
{
  [Export] double speed = 0.5;
  // Called when the node enters the scene tree for the first time.

  private Vector3 _targetPosition;
  private CharacterVisuals targetCharacterVisuals;

  public override void _Ready()
  {
    GetNode<Area3D>("Area3D").BodyEntered += _on_Area3D_body_entered;
  }

  public void Init(Vector3 From, Vector3 To, CharacterVisuals targetCharacterVisuals){
    Position = From;
    _targetPosition = To;
    this.targetCharacterVisuals = targetCharacterVisuals;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    Position = Position + (_targetPosition - Position) * (float)(delta*speed);
// Doesn't work properly, maybe simple collision box?
    // if((Position - _targetPosition) < new Vector3(0.01f, 0.01f, 0.01f)) {
    //   QueueFree();
    // }
  }

  public void _on_Area3D_body_entered(Node _body) {
    if(_body == targetCharacterVisuals){
      GD.Print("BOOM");
      QueueFree();
    }
  }
}

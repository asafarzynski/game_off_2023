using Godot;

namespace GameOff2023.Scripts.Characters;

public partial class CharacterVisuals : Node3D
{
    private AnimationPlayer _animationPlayer;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationTreePlayback;
    private Sprite3D _sprite;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
        _animationTree = GetNode<AnimationTree>("%AnimationTree");
        _animationTreePlayback = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
        _sprite = GetNode<Sprite3D>("%Sprite3D");

        _animationTree.Active = true;
    }

    public void SetAnimationSpeed(float speed)
    {
        _animationPlayer.SpeedScale = speed;
    }

    public void AnimateAttack()
    {
        _animationTreePlayback.Travel("Attack");
    }

    public void AnimateWalk(bool isWalking)
    {
        _animationTreePlayback.Travel(isWalking ? "Walk" : "Idle");
    }

    public void SetRotation(bool isLeft = false)
    {
        _sprite.FlipH = isLeft;
    }

    public void AnimateHurt()
    {
        if (_animationTreePlayback.GetCurrentNode() == "Dead")
            return;
        _animationTreePlayback.Travel("Hurt");
    }

    public void AnimateDeath()
    {
        _animationTreePlayback.Travel("Dead");
    }
}

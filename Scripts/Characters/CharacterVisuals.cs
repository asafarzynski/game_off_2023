using Godot;

namespace GameOff2023.Scripts.Characters;

public partial class CharacterVisuals : Node3D
{
    private AnimationPlayer _animationPlayer;
    private AnimationTree _animationTree;
    private Sprite3D _sprite;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
        _animationTree = GetNode<AnimationTree>("%AnimationTree");
        _sprite = GetNode<Sprite3D>("%Sprite3D");

        _animationTree.Active = true;
    }

    public void AnimateAttack()
    {
        _animationTree.SetCondition("attack", true);
    }

    public void AnimateWalk(bool isWalking)
    {
        _animationTree.SetCondition("walk", isWalking);
    }

    public void SetRotation(bool isLeft = false)
    {
        _sprite.FlipH = isLeft;
    }
}

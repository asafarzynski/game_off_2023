using System.Collections.Generic;
using GameOff2023.Scripts.Characters;
using Godot;
using GameOff2023.Scripts.GameplayCore.Spells;


namespace GameOff2023.Scripts.LevelVisuals;

public partial class LevelVisuals : Node3D
{
    public Camera3D Camera { get; private set; }
    public CharactersManager CharactersManager { get; private set; }

    private Dictionary<VFX, CharacterVisuals> _vfxTarget = new();

    public void AnimateSpell(Spell spell, CharacterVisuals targetCharacterVisuals, Vector3 from, Vector3 to)
    {
        if (spell.VFX is null) { }
        else
        {
            var vfxScene = spell.VFX;
            VFX vfxNode = vfxScene.Instantiate<VFX>();
            AddChild(vfxNode);

            vfxNode.Init(from + new Vector3(0, 2.0f, 0), to, targetCharacterVisuals);
            vfxNode.OnReachedTarget += VfxNode_OnReachedTarget;
            vfxNode.OnEnded += VfxNode_OnEnded;

            _vfxTarget.Add(vfxNode, targetCharacterVisuals);
        }
    }

    public override void _Ready()
    {
        base._Ready();
        CharactersManager = GetNode<CharactersManager>("%CharactersManager");
        Camera = GetNode<Camera3D>("%Camera");
    }

    private void VfxNode_OnReachedTarget(VFX vfx)
    {
        _vfxTarget[vfx].AnimateHurt();
    }

    private void VfxNode_OnEnded(VFX vfx)
    {
        vfx.QueueFree();
        _vfxTarget.Remove(vfx);
    }
}

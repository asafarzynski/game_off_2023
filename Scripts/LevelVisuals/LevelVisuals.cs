using GameOff2023.Scripts.Characters;
using Godot;
using GameOff2023.Scripts.GameplayCore.Spells;


namespace GameOff2023.Scripts.LevelVisuals;

public partial class LevelVisuals : Node3D
{
    public Camera3D Camera { get; private set; }
    public CharactersManager CharactersManager { get; private set; }

    public void AnimateSpell(Spell spell, Vector3 from, Vector3 to) {
        if(spell.VFX is null){
        } else {
            var vfxScene = spell.VFX;
            VFX vfxNode = vfxScene.Instantiate<VFX>();
            vfxNode.Init(from + new Vector3(0, 2.0f, 0), to);
            AddChild(vfxNode);
        }

    }
    
    public override void _Ready()
    {
        base._Ready();
        CharactersManager = GetNode<CharactersManager>("%CharactersManager");
        Camera = GetNode<Camera3D>("%Camera");
    }
}

using GameOff2023.Scripts.Characters;
using Godot;

namespace GameOff2023.Scripts.LevelVisuals;

public partial class LevelVisuals : Node3D
{
    public CharactersManager CharactersManager { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();
        CharactersManager = GetNode<CharactersManager>("%CharactersManager");
    }
}

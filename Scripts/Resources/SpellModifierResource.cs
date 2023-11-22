using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellModifierResource : Resource, IIconProvider, INameProvider, IDescriptionProvider
{
    [Export] private SpellModifierElementResource[] modifierElements;
    [Export] public Texture2D Icon { get; private set; }
    [Export] public string Name { get; private set; }
    [Export] public string Description { get; private set; }
}
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellModifierResource : Resource, IIconContainer
{
    [Export] private SpellModifierElementResource[] modifierElements;
    [Export] private Texture2D icon;

    public Texture2D GetIcon() => icon;
}
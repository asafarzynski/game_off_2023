using System.Linq;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellModifierResource : Resource, IIconProvider, INameProvider, IDescriptionProvider
{
    [ExportGroup("Data")]
    [Export] private SpellModifierElementResource[] modifierElements;
    
    [ExportGroup("Visuals")]
    [Export] public Texture2D Icon { get; private set; }
    [Export] public string Name { get; private set; }
    [Export] public string Description { get; private set; }

    public SpellModifier ToSpellModifier(ResourceId resourceId)
    {
        return new SpellModifier(resourceId, modifierElements.Select(elementResource => elementResource.ToModifierElement()).ToArray());
    }
}
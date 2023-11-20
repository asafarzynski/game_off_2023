using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellResource : Resource, IIconContainer
{
    [ExportGroup("Core Data")]
    [Export] private int Cooldown;
    [Export] private int Count;
    [Export] private float Damage;
    [Export] private float CriticalChance;
    [Export] private SpellTarget Target;

    [ExportGroup("Visuals")] [Export] public Texture2D Icon;

    public Spell ToSpell(ResourceId resourceId) => new Spell(resourceId, Cooldown, Count, Damage, CriticalChance, Target);

    public Texture2D GetIcon() => Icon;
}

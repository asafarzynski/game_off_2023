using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellResource : Resource, IIconContainer
{
	[ExportGroup("Core Data")]
	[Export] public float Cooldown;
	[Export] public int Count;
	[Export] public float Damage;
	[Export] public float CriticalChance;
	
	[ExportGroup("Visuals")]
	[Export] public Texture2D Icon;

	public Spell ToSpell(ResourceId resourceId) => new Spell(resourceId, Cooldown, Count, Damage, CriticalChance);
	
	public Texture2D GetIcon() => Icon;
}

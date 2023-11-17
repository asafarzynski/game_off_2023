using Godot;
using System.Linq;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Enemies;
namespace GameOff2023.Scripts.Resources;

public partial class EnemyResource : Resource
{
	[Export] private string Name;
	[Export] private int Health;
	[Export] private SpellResource[] SpellsList;

	public Enemy ToEnemy(ResourceId resourceId) => new Enemy(resourceId)
	{
		Name = Name,
		Health = Health,
		SpellsList = SpellsList.Select(spellResource => spellResource.ToSpell(ResourcesManager.GetId("Spell"))).ToList(),
	};
}

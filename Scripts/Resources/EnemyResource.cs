using Godot;
using System.Linq;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.GameplayCore.Enemies;
namespace GameOff2023.Scripts.Resources;

public partial class EnemyResource : Resource
{
	[Export] public string Name;
	[Export] public int Health;
	[Export] public SpellResource[] SpellsList;

	public Enemy ToEnemy() => new Enemy()
	{
		Name = Name,
		Health = Health,
		SpellsList = SpellsList.Select(spellResource => spellResource.ToSpell()).ToList(),
	};
}

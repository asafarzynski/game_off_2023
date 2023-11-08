using System.Collections.Generic;
using Godot;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Enemies;

public struct Enemy
{
	public string Name;
	public int Health;
	public List<Spell> SpellsList;

	public Enemy() : this("Little Slime", 2137, new List<Spell>())
	{}

	public Enemy(string name, int health, List<Spell> spellsList)
	{
		Name = name;
		Health = health;
		SpellsList = spellsList;
	}

	public override string ToString()
	{
		return $"-----\nName: {Name}\nHealth: {Health}\nSpells: {SpellsList}\n-----";
	}
}

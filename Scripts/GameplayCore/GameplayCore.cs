using System.Collections.Generic;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.GameplayCore.Enemies;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
	public readonly CommandsExecutioner CommandsExecutioner = new();
	
	public float Score { get; internal set; } = 1f;
	
	public List<Spell> AvailableSpells { get; private set; }

	public List<Enemy> Enemies { get; private set; }

	public Dictionary<float, List<Spell>> SpellsStack { get; private set; } = new Dictionary<float, List<Spell>>();

	public string SpellStackString()
	// TODO - separate class for spellsStack and move it there as toString?
		{
			string result = "";

			foreach (var kvp in SpellsStack)
			{
				result += $"{kvp.Key} -> ";

				List<string> spellNames = new List<string>();
				foreach (var spell in kvp.Value)
				{
					spellNames.Add(spell.ToString());
				}

				result += string.Join(", \n", spellNames) + "\n";
			}

			return result;
		}

	public GameplayCore(List<Spell> spells)
	{
		AvailableSpells = spells;
	}
}

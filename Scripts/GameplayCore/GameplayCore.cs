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

	public List<Spell> SpellsStack { get; private set; } = new List<Spell>(); 

	public GameplayCore(List<Spell> spells)
	{
		AvailableSpells = spells;
	}
}

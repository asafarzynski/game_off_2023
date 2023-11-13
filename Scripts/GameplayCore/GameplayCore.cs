using System.Collections.Generic;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.GameplayCore.Enemies;
using GameOff2023.Scripts.GameplayCore.Player;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
	public readonly CommandsExecutioner CommandsExecutioner = new();
	
	public float Score { get; internal set; } = 1f;
	
	/// <summary>
	/// A list of all spells in game.
	/// We will draw random spells from this list as rewards.
	/// </summary>
	public List<Spell> AllSpellsInGame { get; private set; }

	/// <summary>
	/// A list of all enemies in game.
	/// We will draw random enemies from this list for battles.
	/// </summary>
	public List<Enemy> Enemies { get; private set; } // TODO: We should divide them between levels or something to make the game progressively harder (?) / levels more distinctive

	public readonly PlayerInventory Inventory;
	
	public readonly SpellStack SpellStack;

	public GameplayCore(List<Spell> spellsInGame, List<Enemy> enemiesInGame)
	{
		Enemies = enemiesInGame;
		AllSpellsInGame = spellsInGame;
		SpellStack = new SpellStack();
		Inventory = new PlayerInventory();
	}
}

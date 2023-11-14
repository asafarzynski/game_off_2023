using System.Collections.Generic;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.GameplayCore.Enemies;
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
	public Spell[] AllSpellsInGame { get; private set; }

	/// <summary>
	/// A list of all enemies in game.
	/// We will draw random enemies from this list for battles.
	/// </summary>
	public Enemy[] Enemies { get; private set; } // TODO: We should divide them between levels or something to make the game progressively harder (?) / levels more distinctive

	public readonly Inventory.Inventory Inventory;
	
	public readonly SpellStack SpellStack;
	public LevelManager levels;

	public readonly GameplayCoreEvents Events;

	public GameplayCore(Spell[] spellsInGame, Enemy[] enemiesInGame)
	{
		Enemies = enemiesInGame;
		AllSpellsInGame = spellsInGame;
		SpellStack = new SpellStack();
		Inventory = new Inventory.Inventory();
		Events = new GameplayCoreEvents();
	}
	
	public void CreateNewLevel()
	{
		levels.GenerateLevel();
	}
}

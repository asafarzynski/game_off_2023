using System.Collections.Generic;
using Godot;
using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.GameplayCore.Enemies;
using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
	public readonly CommandsExecutioner CommandsExecutioner = new();
	
	public float Score { get; internal set; } = 1f;
	public float PlayerHealth { get; internal set; } = 100f;

	/// <summary>
	/// A list of all spells in game.
	/// We will draw random spells from this list as rewards.
	/// </summary>
	public readonly Spell[] AllSpellsInGame;

	/// <summary>
	/// A list of all enemies in game.
	/// We will draw random enemies from this list for battles.
	/// </summary>
	public readonly Enemy[] Enemies;

	public readonly Inventory.Inventory Inventory;
	
	public readonly SpellStack SpellStack;
	public readonly LevelManager LevelManager;

	public readonly GameplayCoreEvents Events;

	public GameplayCore(Spell[] spellsInGame, Enemy[] enemiesInGame)
	{
		Enemies = enemiesInGame;
		AllSpellsInGame = spellsInGame;
		SpellStack = new SpellStack();
		Inventory = new Inventory.Inventory();
		Events = new GameplayCoreEvents();
		LevelManager = new LevelManager(Enemies);
		
		Initialize();
	}

	private void Initialize()
	{
		CommandsExecutioner.Do(new GivePlayerRandomSpellCommand(this));
		LevelManager.GenerateNextLevel();
	}
}

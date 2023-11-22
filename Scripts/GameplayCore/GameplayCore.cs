using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
	public readonly CommandsExecutioner CommandsExecutioner = new();

	public readonly Character PlayerCharacter;

	/// <summary>
	/// A list of all spells in game.
	/// We will draw random spells from this list as rewards.
	/// </summary>
	public readonly Spell[] AllSpellsInGame;

	/// <summary>
	/// A list of all enemies in game.
	/// We will draw random enemies from this list for battles.
	/// </summary>
	public readonly Character[] Enemies;

	public readonly Inventory.Inventory Inventory;
	
	public readonly LevelManager LevelManager;

	public readonly GameplayCoreEvents Events;

	public GameplayCore(Character playerCharacter, Spell[] spellsInGame, Character[] enemiesInGame)
	{
		Enemies = enemiesInGame;
		AllSpellsInGame = spellsInGame;
		Inventory = new Inventory.Inventory();
		Events = new GameplayCoreEvents();
		LevelManager = new LevelManager(Enemies);
		PlayerCharacter = playerCharacter;
		
		Initialize();
	}

	private void Initialize()
	{
		LevelManager.GenerateNextLevel();
		for (var i = 0; i < PlayerCharacter.Spells.Count; i++)
		{
			Inventory.SpellSlots[i].Spell = PlayerCharacter.Spells[i];
		}
	}
}

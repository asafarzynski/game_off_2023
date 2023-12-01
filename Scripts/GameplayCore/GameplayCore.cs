using GameOff2023.Scripts.Commands;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
	public readonly CommandsExecutioner CommandsExecutioner = new();

	public readonly FightingCharacter PlayerCharacter;

	/// <summary>
	/// A list of all spells in game.
	/// We will draw random spells from this list as rewards.
	/// </summary>
	public readonly Spell[] AllSpellsInGame;

	/// <summary>
	/// A list of all spell modifiers in game.
	/// We will draw random spell modifiers from this list as rewards.
	/// </summary>
	public readonly SpellModifier[] AllSpellModifiersInGame;

	/// <summary>
	/// A list of all enemies in game.
	/// We will draw random enemies from this list for battles.
	/// </summary>
	public readonly Character[] Enemies;

	public readonly FightingCharacter[] FinalBosses;

	public readonly Inventory.Inventory Inventory;
	
	public readonly LevelManager LevelManager;

	public readonly GameplayCoreEvents Events;

	public readonly CharactersDictionary CharactersDictionary;
    
	private static readonly SimpleIdGenerator<FightingCharacter> FightingCharactersIdGenerator = new();

	public GameplayCore(Character playerCharacter,
		Spell[] spellsInGame,
		SpellModifier[] spellModifiers,
		Character[] enemiesInGame,
		Character[] finalBosses)
	{
		PlayerCharacter = PreparePlayerCharacter(playerCharacter);
		Enemies = enemiesInGame;
		FinalBosses = PrepareBossCharacters(finalBosses);
		AllSpellsInGame = spellsInGame;
		AllSpellModifiersInGame = spellModifiers;
		
		Inventory = new Inventory.Inventory();
		Events = new GameplayCoreEvents();
		CharactersDictionary = new CharactersDictionary();
		CharactersDictionary.RegisterCharacter(PlayerCharacter);
		LevelManager = new LevelManager(Enemies, FightingCharactersIdGenerator, CharactersDictionary);
		
		Initialize();
	}

	private FightingCharacter PreparePlayerCharacter(Character character)
	{
		return new FightingCharacter()
		{
			Character = character,
			Id = FightingCharactersIdGenerator.GetNextId(),
			FightStatus = new CharacterFightStatus(character.Stats),
		};
	}

	private FightingCharacter[] PrepareBossCharacters(Character[] characters)
	{
		var fightingCharacters = new FightingCharacter[characters.Length];
		for (int i = 0; i < characters.Length; i++)
		{
			fightingCharacters[i] = new FightingCharacter()
			{
				Character = characters[i],
				Id = FightingCharactersIdGenerator.GetNextId(),
				FightStatus = new CharacterFightStatus(characters[i].Stats),
			};
		}
		return fightingCharacters;
	}

	private void Initialize()
	{
		LevelManager.GenerateNextLevel();
		for (var i = 0; i < PlayerCharacter.Character.Spells.Count; i++)
		{
			Inventory.SpellSlots[i].Spell = PlayerCharacter.Character.Spells[i];
		}
	}
}

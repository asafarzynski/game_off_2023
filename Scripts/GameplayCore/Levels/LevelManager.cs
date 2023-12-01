using Godot;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Id;

namespace GameOff2023.Scripts.GameplayCore.Levels;

public class LevelManager
{
    public const int MIN_BOSS_LEVEL = 9;
    public readonly List<Level> Levels = new List<Level>();
    public Level CurrentLevel => Levels[^1];
    public bool ReadyForBoss => Levels.Count >= MIN_BOSS_LEVEL;
    public Fight CurrentFight => CurrentLevel.FightList[CurrentLevel.CurrentFightIndex];

    private readonly Character[] _enemies;
    private readonly SimpleIdGenerator<FightingCharacter> _idGenerator;
    private readonly CharactersDictionary _charactersDictionary;
    private readonly RandomNumberGenerator _random;

    public LevelManager(Character[] enemies, SimpleIdGenerator<FightingCharacter> idGenerator, CharactersDictionary charactersDictionary)
    {
        _enemies = enemies;
        _idGenerator = idGenerator;
        _charactersDictionary = charactersDictionary;
        _random = new RandomNumberGenerator();
        _random.Randomize();
    }

    internal Level GenerateRandomLevel()
    {
        Level level = new Level(Levels.Count);

        for (int x = 0; x < level.FightList.Length; x++)
        {
            Fight fight = new Fight(x);
            for (int y = 0; y < fight.EnemyList.Length; y++)
            {
                var randomValue = _random.RandiRange(0, _enemies.Length - 1);

                var fightingCharacter = new FightingCharacter()
                {
                    Id = _idGenerator.GetNextId(),
                    Character = _enemies[randomValue],
                    FightStatus = new CharacterFightStatus(_enemies[randomValue].Stats),
                };
                fight.EnemyList[y] = fightingCharacter;
                _charactersDictionary.RegisterCharacter(fightingCharacter);
            }
            level.FightList[x] = fight;
        }
        Levels.Add(level);
        return level;
    }

    internal Level AddSpecificLevel(FightingCharacter[] fightingCharacters)
    {
        foreach (var character in fightingCharacters)
        {
            _charactersDictionary.RegisterCharacter(character);
        }

        Level level = new Level(Levels.Count, new[] { new Fight(0, fightingCharacters) });
        Levels.Add(level);

        return level;
    }
}

using Godot;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Id;

namespace GameOff2023.Scripts.GameplayCore.Levels;

public class LevelManager
{
    public readonly List<Level> Levels = new List<Level>();
    public Level CurrentLevel => Levels[^1];
    public Fight CurrentFight => CurrentLevel.FightList[CurrentLevel.CurrentFightIndex];

    private readonly Character[] _enemies;
    private readonly SimpleIdGenerator<FightingCharacter> _idGenerator;
    private readonly CharactersDictionary _charactersDictionary;

    public LevelManager(Character[] enemies, SimpleIdGenerator<FightingCharacter> idGenerator, CharactersDictionary charactersDictionary)
    {
        _enemies = enemies;
        _idGenerator = idGenerator;
        _charactersDictionary = charactersDictionary;
    }

    internal Level GenerateNextLevel()
    {
        Level level = new Level(Levels.Count);

        for (int x = 0; x < level.FightList.Length; x++)
        {
            Fight fight = new Fight(x);
            for (int y = 0; y < fight.EnemyList.Length; y++)
            {
                var randomGenerator = new RandomNumberGenerator();
                randomGenerator.Randomize();
                var randomValue = randomGenerator.RandiRange(0, _enemies.Length-1);

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
}

using System;
using System.Collections.Generic;
using System.Linq;
using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.Fight;

public enum CharacterType
{
    Player,
    Monster
}

public enum SpelllTarget
{
    Self,
    FirstEnemy,
    // Other possible targets can be added here
    // RandomEnemy,
    // MinHealthEnemy,
    // LastEnemy
}

public class Character
{
    public CharacterType CharacterType { get; set; }
    public List<Spelll> Spellls { get; set; }
    public string Name { get; set; }
    public CharacterStats Stats { get; set; }
}

public class CharacterStats
{
    public int Health { get; set; }
}

public class Spelll
{
    public int Dmg { get; set; }
    public int Cooldown { get; set; } // Multiples of 2: 1, 2, 4, 8, 16, etc.
    public SpelllTarget Target { get; set; }
}

public class SpelllCast : Spelll
{
    public Character OriginCharacter { get; set; }
}

public enum FightStatus
{
    Win,
    Lose,
    Draw,
    InProgress
}

public enum FightEventType
{
    FightStart,
    FightResult,
    SpelllCast,
    CharacterDeath
}

public class FightEvent
{
    public FightEventType EventType { get; set; }
    public FightStatus? FightResult { get; set; }
    public SpelllCast SpelllCast { get; set; }
    public Character TargetCharacter { get; set; }
    public CharacterFightStatus TargetCharacterFightStatus { get; set; }
}

public class CharacterFightStatus
{
    public int Health { get; set; }
    // Other effect properties...
}

public partial class FightSimulator: Node
{
    public static List<FightEvent> SimulateFight(List<Character> playerCharacters, List<Character> monsterCharacters)
    {
        var SpelllCastQueue = new List<SpelllCast>();
        var fightEventQueue = new List<FightEvent> { new FightEvent { EventType = FightEventType.FightStart } };
        var characterFightStatusMap = new Dictionary<Character, CharacterFightStatus>();

        foreach (var character in playerCharacters.Concat(monsterCharacters))
        {
            characterFightStatusMap[character] = new CharacterFightStatus { Health = character.Stats.Health };
            AddSpelllsToTheQueue(character, SpelllCastQueue);
        }

        SpelllCastQueue.Sort(delegate(SpelllCast a, SpelllCast b){
            var cooldownDifference = a.Cooldown - b.Cooldown;
            if(cooldownDifference == 0) {
                if(a.OriginCharacter.CharacterType == CharacterType.Player && b.OriginCharacter.CharacterType == CharacterType.Monster) {
                    return -1;
                }

                if(a.OriginCharacter.CharacterType == CharacterType.Monster && b.OriginCharacter.CharacterType == CharacterType.Player) {
                    return 1;
                }
            }
            
            return cooldownDifference;
        });

        var fightStatus = FightStatus.InProgress;

        for (var SpelllIndex = 0; SpelllIndex < SpelllCastQueue.Count; SpelllIndex++)
        {
            var SpelllCast = SpelllCastQueue[SpelllIndex];
            Character targetCharacter = null;

            if (characterFightStatusMap[SpelllCast.OriginCharacter].Health <= 0)
                continue;

            switch (SpelllCast.Target)
            {
                case SpelllTarget.Self:
                    targetCharacter = SpelllCast.OriginCharacter;
                    break;
                case SpelllTarget.FirstEnemy:
                    if (SpelllCast.OriginCharacter.CharacterType == CharacterType.Player)
                    {
                        var monster = monsterCharacters.FirstOrDefault(monsterCharacter => characterFightStatusMap[monsterCharacter].Health > 0);
                        if (monster != null)
                            targetCharacter = monster;
                        else
                            fightStatus = FightStatus.Win;
                    }
                    if (SpelllCast.OriginCharacter.CharacterType == CharacterType.Monster)
                    {
                        var player = playerCharacters.FirstOrDefault(playerCharacter => characterFightStatusMap[playerCharacter].Health > 0);
                        if (player != null)
                            targetCharacter = player;
                        else
                            fightStatus = FightStatus.Lose;
                    }
                    break;
            }

            if (targetCharacter != null)
            {
                characterFightStatusMap[targetCharacter].Health = DealDamage(targetCharacter, characterFightStatusMap[targetCharacter], SpelllCast.Dmg);
                fightEventQueue.Add(new FightEvent
                {
                    EventType = FightEventType.SpelllCast,
                    SpelllCast = SpelllCast,
                    TargetCharacter = targetCharacter,
                    TargetCharacterFightStatus = characterFightStatusMap[targetCharacter]
                });

                if (characterFightStatusMap[targetCharacter].Health == 0)
                {
                    fightEventQueue.Add(new FightEvent
                    {
                        EventType = FightEventType.CharacterDeath,
                        TargetCharacter = targetCharacter
                    });
                }
            }

            if (fightStatus != FightStatus.InProgress)
                break;
        }

        if (fightStatus == FightStatus.InProgress)
            fightStatus = FightStatus.Draw;

        fightEventQueue.Add(new FightEvent { EventType = FightEventType.FightResult, FightResult = fightStatus });

        return fightEventQueue;
    }

    public static void AddSpelllsToTheQueue(Character character, List<SpelllCast> queue)
    {
        foreach (var Spelll in character.Spellls)
        {
            var numberOfSpelllsCast = (int)Math.Floor(256 / (double)Spelll.Cooldown);
            for (var i = 0; i < numberOfSpelllsCast; i++)
            {
                queue.Add(new SpelllCast
                {
                    Dmg = Spelll.Dmg,
                    Cooldown = Spelll.Cooldown * (i + 1),
                    Target = Spelll.Target,
                    OriginCharacter = character
                });
            }
        }
    }

    public static int DealDamage(Character character, CharacterFightStatus characterFightStatus, int damage)
    {
        return Clamp(0, character.Stats.Health, characterFightStatus.Health - damage);
    }

    public static int Clamp(int min, int max, int value)
    {
        return Math.Max(Math.Min(max, value), min);
    }

    public static void LogFightEvent(FightEvent fightEvent)
    {
        switch (fightEvent.EventType) {
            case FightEventType.FightStart: {
                GD.Print("Start of the fight!");
                break;
            }
            case FightEventType.FightResult: {
                GD.Print($"Fight result: {fightEvent.FightResult}");
                break;
            }
            case FightEventType.CharacterDeath: {
                GD.Print($"{fightEvent.TargetCharacter.Name} has died :(");
                break;
            }
            case FightEventType.SpelllCast: {
                LogHit(
                    fightEvent.SpelllCast.OriginCharacter,
                    fightEvent.TargetCharacter,
                    fightEvent.TargetCharacterFightStatus,
                    fightEvent.SpelllCast.Dmg
                );
                break;
            }
            default:
                GD.Print("Unsupported fight event");
                break;
        }
    }

    public static void LogHit(Character originCharacter, Character targetCharacter, CharacterFightStatus targetCharacterFightStatus, float damage) {
        if(damage < 0) {
            GD.Print($"{originCharacter.Name} heals {targetCharacter.Name} for {Math.Abs(damage)}");
        } else {
            GD.Print($"{originCharacter.Name} deals {damage} to {targetCharacter.Name}");
        }
    }

    public static bool Simulate()
    {
        GD.Print("FIGHT!!");
        var player = new Character
        {
            Name = "Grzybiarz",
            CharacterType = CharacterType.Player,
            Spellls = new List<Spelll> { new Spelll { Dmg = 5, Cooldown = 1, Target = SpelllTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 100 }
        };

        var enemyTroll = new Character
        {
            Name = "Troll",
            CharacterType = CharacterType.Monster,
            Spellls = new List<Spelll>
            {
                new Spelll { Dmg = -2, Cooldown = 2, Target = SpelllTarget.Self },
                new Spelll { Dmg = 3, Cooldown = 4, Target = SpelllTarget.FirstEnemy }
            },
            Stats = new CharacterStats { Health = 200 }
        };

        var enemyOrc = new Character
        {
            Name = "Orc",
            CharacterType = CharacterType.Monster,
            Spellls = new List<Spelll> { new Spelll { Dmg = 3, Cooldown = 4, Target = SpelllTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        };
        var enemyOrc2 = new Character
        {
            Name = "Orc2",
            CharacterType = CharacterType.Monster,
            Spellls = new List<Spelll> { new Spelll { Dmg = 3, Cooldown = 4, Target = SpelllTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        };
        var enemyOrc3 = new Character
        {
            Name = "Orc3",
            CharacterType = CharacterType.Monster,
            Spellls = new List<Spelll> { new Spelll { Dmg = 3, Cooldown = 4, Target = SpelllTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        }; 

        var fightEvents = SimulateFight(new List<Character> { player }, new List<Character> { enemyOrc, enemyOrc2, enemyOrc3, enemyTroll });

        foreach (var fightEvent in fightEvents)
        {
            LogFightEvent(fightEvent);
        }

        if(fightEvents[^1].FightResult == FightStatus.Win) {
            return true;
        } else if(fightEvents[^1].FightResult == FightStatus.Draw) {
            return false;
        } else {
            return false;
        }
    }
}

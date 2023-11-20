using System;
using System.Collections.Generic;
using System.Linq;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.Fight;

public static class FightSimulator
{
    public static List<FightEvent> SimulateFight(Character[] playerCharacters, Character[] monsterCharacters)
    {
        var SpellCastQueue = new List<SpellCast>();
        var fightEventQueue = new List<FightEvent> { new FightEvent { EventType = FightEventType.FightStart } };
        var characterFightStatusMap = new Dictionary<Character, CharacterFightStatus>();

        foreach (var character in playerCharacters.Concat(monsterCharacters))
        {
            characterFightStatusMap[character] = new CharacterFightStatus { Health = character.Stats.Health };
            AddSpellsToTheQueue(character, SpellCastQueue);
        }

        SpellCastQueue.Sort(delegate(SpellCast a, SpellCast b){
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

        for (var SpellIndex = 0; SpellIndex < SpellCastQueue.Count; SpellIndex++)
        {
            var SpellCast = SpellCastQueue[SpellIndex];
            Character targetCharacter = null;

            if (characterFightStatusMap[SpellCast.OriginCharacter].Health <= 0)
                continue;

            switch (SpellCast.Spell.Target)
            {
                case SpellTarget.Self:
                    targetCharacter = SpellCast.OriginCharacter;
                    break;
                case SpellTarget.FirstEnemy:
                    if (SpellCast.OriginCharacter.CharacterType == CharacterType.Player)
                    {
                        var monster = monsterCharacters.FirstOrDefault(monsterCharacter => characterFightStatusMap[monsterCharacter].Health > 0);
                        if (monster != null)
                            targetCharacter = monster;
                        else
                            fightStatus = FightStatus.Win;
                    }
                    if (SpellCast.OriginCharacter.CharacterType == CharacterType.Monster)
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
                characterFightStatusMap[targetCharacter].Health = DealDamage(targetCharacter, characterFightStatusMap[targetCharacter], SpellCast.Spell.Damage);
                fightEventQueue.Add(new FightEvent
                {
                    EventType = FightEventType.SpellCast,
                    SpellCast = SpellCast,
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

    public static void AddSpellsToTheQueue(Character character, List<SpellCast> queue)
    {
        foreach (var Spell in character.Spells)
        {
            var numberOfSpellsCast = (int)Math.Floor(256 / (double)Spell.Cooldown);
            for (var i = 0; i < numberOfSpellsCast; i++)
            {
                queue.Add(new SpellCast
                {
                    Spell = Spell,
                    Cooldown = Spell.Cooldown * (i + 1),
                    OriginCharacter = character
                });
            }
        }
    }

    public static float DealDamage(Character character, CharacterFightStatus characterFightStatus, float damage)
    {
        return Math.Clamp(characterFightStatus.Health - damage, 0, character.Stats.Health);
    }

    public static string LogFightEvent(FightEvent fightEvent)
    {
        switch (fightEvent.EventType) {
            case FightEventType.FightStart: {
                return "Start of the fight!";
            }
            case FightEventType.FightResult: {
                return $"Fight result: {fightEvent.FightResult}";
            }
            case FightEventType.CharacterDeath: {
                return $"{fightEvent.TargetCharacter.Name} has died :(";
            }
            case FightEventType.SpellCast: {
                return LogHit(
                    fightEvent.SpellCast.OriginCharacter,
                    fightEvent.TargetCharacter,
                    fightEvent.TargetCharacterFightStatus,
                    fightEvent.SpellCast.Spell.Damage
                );
            }
            default:
                return "Unsupported fight event";
        }
    }

    public static string LogHit(Character originCharacter, Character targetCharacter, CharacterFightStatus targetCharacterFightStatus, float damage) {
        if(damage < 0) {
            return $"{originCharacter.Name} heals {targetCharacter.Name} for {Math.Abs(damage)}";
        } else {
            return $"{originCharacter.Name} deals {damage} to {targetCharacter.Name}";
        }
    }

    public static List<FightEvent> Simulate()
    {
        var player = new Character
        {
            Name = "Grzybiarz",
            CharacterType = CharacterType.Player,
            Spells = new List<Spell> { new Spell { Damage = 5, Cooldown = 1, Target = SpellTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 100 }
        };

        var enemyTroll = new Character
        {
            Name = "Troll",
            CharacterType = CharacterType.Monster,
            Spells = new List<Spell>
            {
                new Spell { Damage = -2, Cooldown = 2, Target = SpellTarget.Self },
                new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy }
            },
            Stats = new CharacterStats { Health = 200 }
        };

        var enemyOrc = new Character
        {
            Name = "Orc",
            CharacterType = CharacterType.Monster,
            Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        };
        var enemyOrc2 = new Character
        {
            Name = "Orc2",
            CharacterType = CharacterType.Monster,
            Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        };
        var enemyOrc3 = new Character
        {
            Name = "Orc3",
            CharacterType = CharacterType.Monster,
            Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
            Stats = new CharacterStats { Health = 50 }
        }; 

        var fightEvents = SimulateFight(new Character[] { player }, new Character[] { enemyOrc, enemyOrc2, enemyOrc3, enemyTroll });

        return fightEvents;
    }
}

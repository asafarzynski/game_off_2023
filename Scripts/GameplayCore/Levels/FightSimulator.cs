using System;
using System.Collections.Generic;
using System.Linq;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Id;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.Fight;

public static class FightSimulator
{
    public static List<FightEvent> SimulateFight(FightingCharacter[] playerCharacters, FightingCharacter[] monsterCharacters)
    {
        var spellCastQueue = new List<SpellCast>();
        var fightEventQueue = new List<FightEvent> { new FightEvent { EventType = FightEventType.FightStart } };
        var fightingCharacters = new Dictionary<ID<FightingCharacter, int>, FightingCharacter>();
        var playersFighting = new FightingCharacter[playerCharacters.Length];
        var monstersFighting = new FightingCharacter[monsterCharacters.Length];

        for (var index = 0; index < playerCharacters.Length; index++)
        {
            var fightingCharacter = playerCharacters[index];
            fightingCharacters.Add(fightingCharacter.Id, fightingCharacter);
            AddSpellsToTheQueue(fightingCharacter, spellCastQueue);
            playersFighting[index] = fightingCharacter;
        }

        for (var index = 0; index < monsterCharacters.Length; index++)
        {
            var fightingCharacter = monsterCharacters[index];
            fightingCharacters.Add(fightingCharacter.Id, fightingCharacter);
            AddSpellsToTheQueue(fightingCharacter, spellCastQueue);
            monstersFighting[index] = fightingCharacter;
        }

        spellCastQueue.Sort(delegate(SpellCast a, SpellCast b)
        {
            var cooldownDifference = a.Cooldown - b.Cooldown;
            if (cooldownDifference == 0)
            {
                if (a.OriginCharacter.Character.CharacterType == CharacterType.Player && b.OriginCharacter.Character.CharacterType == CharacterType.Monster)
                {
                    return -1;
                }

                if (a.OriginCharacter.Character.CharacterType == CharacterType.Monster && b.OriginCharacter.Character.CharacterType == CharacterType.Player)
                {
                    return 1;
                }
            }

            return cooldownDifference;
        });

        var fightStatus = FightStatus.InProgress;

        foreach (var spellCast in spellCastQueue)
        {
            FightingCharacter targetCharacter = null;

            if (fightingCharacters[spellCast.OriginCharacter.Id].FightStatus.Health <= 0)
                continue;

            switch (spellCast.Spell.Target)
            {
                case SpellTarget.Self:
                    targetCharacter = spellCast.OriginCharacter;
                    break;
                case SpellTarget.FirstEnemy:
                    if (spellCast.OriginCharacter.Character.CharacterType == CharacterType.Player)
                    {
                        var monster = monstersFighting.FirstOrDefault(monsterCharacter => fightingCharacters[monsterCharacter.Id].FightStatus.Health > 0);
                        if (monster != null)
                            targetCharacter = monster;
                        else
                            fightStatus = FightStatus.Win;
                    }
                    if (spellCast.OriginCharacter.Character.CharacterType == CharacterType.Monster)
                    {
                        var player = playersFighting.FirstOrDefault(playerCharacter => fightingCharacters[playerCharacter.Id].FightStatus.Health > 0);
                        if (player != null)
                            targetCharacter = player;
                        else
                            fightStatus = FightStatus.Lose;
                    }
                    break;
            }

            if (targetCharacter != null)
            {
                fightingCharacters[targetCharacter.Id].FightStatus.Health = DealDamage(targetCharacter, spellCast.Spell.Damage);
                var targetCharacterStatsAfter = fightingCharacters[targetCharacter.Id].FightStatus;
                fightEventQueue.Add(new FightEvent
                {
                    EventType = FightEventType.SpellCast,
                    SpellCast = spellCast,
                    TargetCharacter = targetCharacter,
                    TargetCharacterStatsAfter = targetCharacterStatsAfter,
                });

                if (fightingCharacters[targetCharacter.Id].FightStatus.Health == 0)
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

    private static void AddSpellsToTheQueue(FightingCharacter character, List<SpellCast> queue)
    {
        foreach (var spell in character.Character.Spells)
        {
            var numberOfSpellsCast = (int)Math.Floor(256 / (double)spell.Cooldown);
            for (var i = 0; i < numberOfSpellsCast; i++)
            {
                queue.Add(new SpellCast
                {
                    Spell = spell,
                    Cooldown = spell.Cooldown * (i + 1),
                    OriginCharacter = character,
                });
            }
        }
    }

    private static float DealDamage(FightingCharacter character, float damage)
    {
        return Math.Clamp(character.FightStatus.Health - damage, 0, character.Character.Stats.Health);
    }

    // public static List<FightEvent> Simulate()
    // {
    //     var player = new Character
    //     {
    //         Name = "Grzybiarz",
    //         CharacterType = CharacterType.Player,
    //         Spells = new List<Spell> { new Spell { Damage = 5, Cooldown = 1, Target = SpellTarget.FirstEnemy } },
    //         Stats = new CharacterStats { Health = 100 }
    //     };
    //
    //     var enemyTroll = new Character
    //     {
    //         Name = "Troll",
    //         CharacterType = CharacterType.Monster,
    //         Spells = new List<Spell>
    //         {
    //             new Spell { Damage = -2, Cooldown = 2, Target = SpellTarget.Self },
    //             new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy }
    //         },
    //         Stats = new CharacterStats { Health = 200 }
    //     };
    //
    //     var enemyOrc = new Character
    //     {
    //         Name = "Orc",
    //         CharacterType = CharacterType.Monster,
    //         Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
    //         Stats = new CharacterStats { Health = 50 }
    //     };
    //     var enemyOrc2 = new Character
    //     {
    //         Name = "Orc2",
    //         CharacterType = CharacterType.Monster,
    //         Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
    //         Stats = new CharacterStats { Health = 50 }
    //     };
    //     var enemyOrc3 = new Character
    //     {
    //         Name = "Orc3",
    //         CharacterType = CharacterType.Monster,
    //         Spells = new List<Spell> { new Spell { Damage = 3, Cooldown = 4, Target = SpellTarget.FirstEnemy } },
    //         Stats = new CharacterStats { Health = 50 }
    //     };
    //
    //     var fightEvents = SimulateFight(new Character[] { player }, new Character[] { enemyOrc, enemyOrc2, enemyOrc3, enemyTroll });
    //
    //     return fightEvents;
    // }
}

using System;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.Resources;
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.UI.Battle;

public class FightLogger
{
    public static string LogFightEvent(FightEvent fightEvent)
    {
        switch (fightEvent.EventType)
        {
            case FightEventType.FightStart:
            {
                return "Start of the fight!";
            }
            case FightEventType.FightResult:
            {
                return $"Fight result: {fightEvent.FightResult}";
            }
            case FightEventType.CharacterDeath:
            {
                return $"{fightEvent.TargetCharacter.Name} has died :(";
            }
            case FightEventType.SpellCast:
            {
                return LogHit(
                    fightEvent.SpellCast.OriginCharacter,
                    fightEvent.SpellCast.Spell.ResourceId,
                    fightEvent.TargetCharacter,
                    fightEvent.TargetCharacterFightStatus,
                    fightEvent.SpellCast.Spell.Damage
                );
            }
            default:
                return "Unsupported fight event";
        }
    }

    public static string LogHit(Character originCharacter,
        ResourceId spellResourceId,
        Character targetCharacter,
        CharacterFightStatus targetCharacterFightStatus,
        float damage)
    {
        var spellResource = ResourcesManager.GetResource<Resource>(spellResourceId);

        var spellName = spellResource is INameProvider nameProvider ? nameProvider.Name : "";
        
        if (damage < 0)
        {
            return $"{originCharacter.Name} heals {targetCharacter.Name} for {Math.Abs(damage)} using {spellName}";
        }
        else
        {
            return $"{originCharacter.Name} deals {damage} to {targetCharacter.Name} using {spellName}";
        }
    }
}

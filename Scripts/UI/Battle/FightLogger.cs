using System;
using GameOff2023.Scripts.GameplayCore;
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
                return $"{fightEvent.TargetCharacter.Character.Name} ({fightEvent.TargetCharacter.Id}) has died :(";
            }
            case FightEventType.SpellCast:
            {
                return LogHit(
                    fightEvent.SpellCast.OriginCharacter,
                    fightEvent.SpellCast.Spell.ResourceId,
                    fightEvent.TargetCharacter,
                    fightEvent.SpellCast.Spell.Damage
                );
            }
            default:
                return "Unsupported fight event";
        }
    }

    public static string LogHit(FightingCharacter originCharacter,
        ResourceId spellResourceId,
        FightingCharacter targetCharacter,
        float damage)
    {
        var spellResource = ResourcesManager.GetResource<Resource>(spellResourceId);

        var spellName = spellResource is INameProvider nameProvider ? nameProvider.Name : "";

        return damage < 0
            ? $"{originCharacter.Character.Name} ({originCharacter.Id}) heals {targetCharacter.Character.Name} ({targetCharacter.Id}) for {Math.Abs(damage)} using {spellName}"
            : $"{originCharacter.Character.Name} ({originCharacter.Id}) deals {damage} to {targetCharacter.Character.Name} ({targetCharacter.Id}) using {spellName}";
    }
}

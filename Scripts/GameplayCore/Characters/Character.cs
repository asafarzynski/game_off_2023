using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore.Spells;

namespace GameOff2023.Scripts.GameplayCore.Characters;

public class Character
{
    public ResourceId ResourceId { get; set; }
    public CharacterType CharacterType { get; set; }
    public List<Spell> Spells { get; set; }
    public string Name { get; set; }
    public CharacterStats Stats { get; set; }

    public Character() { }

    public Character(ResourceId resourceId, CharacterType type, string name, List<Spell> spells, CharacterStats stats)
    {
        ResourceId = resourceId;
        CharacterType = type;
        Name = name;
        Spells = spells;
        Stats = stats;
    }

    public override string ToString()
    {
        return $"-----\nName: {Name}\nStats: {Stats}\nSpells: {Spells}\n-----";
    }
}

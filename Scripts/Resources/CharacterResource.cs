using System.Linq;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Characters;
using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class CharacterResource : Resource
{
    [ExportGroup("Data")]
    [Export] private string name;
    [Export] private CharacterType characterType;
    [Export] private int initialHealth;
    [Export] private SpellsList unlockableCharacterSpells;
    [Export] private SpellsList spellbook;

    [ExportGroup("Visuals")]
    [Export] private Texture2D plainTexture;

    public Spell[] GetUnlockableSpells()
    {
        unlockableCharacterSpells.PrepareDictionary();
        return unlockableCharacterSpells.ResourcesDictionary
            .Select((resource, _) => resource.Value.ToSpell(resource.Key))
            .ToArray();
    }

    public Character GetCharacter(ResourceId resourceId)
    {
        return new Character(resourceId,
            characterType,
            name,
            new(GetSpellBook()),
            new CharacterStats()
            {
                Health = initialHealth
            });
    }

    private Spell[] GetSpellBook()
    {
        spellbook.PrepareDictionary();
        return spellbook.ResourcesDictionary
            .Select((resource, _) => resource.Value.ToSpell(resource.Key))
            .ToArray();
    }
}

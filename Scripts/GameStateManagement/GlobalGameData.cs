using GameOff2023.Scripts.Resources;
using GameOff2023.Scripts.Utils;
using Godot;
using System.Linq;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GlobalGameData : NodeSingleton<GlobalGameData>
{
	[Export] public CharactersList HeroesList;
    [Export] public CharactersList EnemiesList;
    [Export] public CharactersList BossesList;
    [Export] public SpellsList SpellsList; // not used right now - we are using per-character defined unlockable spell lists
    [Export] public SpellModifiersList SpellsModifiersList;

    public GameplayCore.GameplayCore Core { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        HeroesList.PrepareDictionary();
        EnemiesList.PrepareDictionary();
        BossesList.PrepareDictionary();
        SpellsList.PrepareDictionary();
        SpellsModifiersList.PrepareDictionary();
    }

    /// <summary>
    /// Add some params, like selected character, etc. in the next step
    /// </summary>
    public void SetUpNewCore()
    {
        var firstPair =  HeroesList.ResourcesDictionary.FirstOrDefault();
        var mainCharacter = firstPair.Value;
        var mainCharacterData = mainCharacter.GetCharacter(firstPair.Key);
        var spellsList = mainCharacter.GetUnlockableSpells();
        var spellModifiers = SpellsModifiersList.ResourcesDictionary.Select((resource, _) => resource.Value.ToSpellModifier(resource.Key)).ToArray();
        var enemies = EnemiesList.ResourcesDictionary.Select((resource, _) => resource.Value.GetCharacter(resource.Key)).ToArray();
        var bosses = BossesList.ResourcesDictionary.Select((resource, _) => resource.Value.GetCharacter(resource.Key)).ToArray();
        
        Core = new GameplayCore.GameplayCore(
            mainCharacterData,
            spellsList,
            spellModifiers,
            enemies,
            bosses);
    }

    public void ClearCore()
    {
        Core = null;
    }
}
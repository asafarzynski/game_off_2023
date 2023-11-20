using GameOff2023.Scripts.Resources;
using GameOff2023.Scripts.Utils;
using Godot;
using System.Linq;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GlobalGameData : NodeSingleton<GlobalGameData>
{
	[Export] public CharactersList HeroesList;
    [Export] public CharactersList EnemiesList;
    [Export] public SpellsList SpellsList; // not used right now - we are using per-character defined unlockable spell lists

    public GameplayCore.GameplayCore Core { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        HeroesList.PrepareDictionary();
        EnemiesList.PrepareDictionary();
        SpellsList.PrepareDictionary();
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
        
        Core = new GameplayCore.GameplayCore(
            mainCharacterData,
            spellsList,
            EnemiesList.ResourcesDictionary.Select((resource, _) => resource.Value.GetCharacter(resource.Key)).ToArray());
    }

    public void ClearCore()
    {
        Core = null;
    }
}
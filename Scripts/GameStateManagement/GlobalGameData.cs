using GameOff2023.Scripts.Resources;
using GameOff2023.Scripts.Utils;
using Godot;
using System.Linq;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GlobalGameData : NodeSingleton<GlobalGameData>
{
	[Export] public SpellsList SpellsList;
	[Export] public EnemiesList EnemiesList;

    public GameplayCore.GameplayCore Core { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        SpellsList.PrepareDictionary();
    }

    /// <summary>
    /// Add some params, like selected character, etc. in the next step
    /// </summary>
    public void SetUpNewCore()
    {
        Core = new GameplayCore.GameplayCore(
            SpellsList.ResourcesDictionary.Select((resource, _) => resource.Value.ToSpell(resource.Key)).ToArray(),
            EnemiesList.GetList().ToArray()); // make it work with resourceid (like spells above)
    }

    public void ClearCore()
    {
        Core = null;
    }
}
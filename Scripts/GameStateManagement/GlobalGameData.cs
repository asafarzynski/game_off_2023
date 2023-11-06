using GameOff2023.Scripts.GameplayCore.Spells;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GlobalGameData : Node
{
    [Export] public SpellsList SpellsList;

    public GameplayCore.GameplayCore Core { get; private set; }
    
    public static GlobalGameData Instance { get; private set; }

    public GlobalGameData()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Free();
        }
    }

    /// <summary>
    /// Add some params, like selected character, etc. in the next step
    /// </summary>
    public void SetUpNewCore()
    {
        Core = new GameplayCore.GameplayCore(SpellsList.GetList());
    }

    public void ClearCore()
    {
        Core = null;
    }
}
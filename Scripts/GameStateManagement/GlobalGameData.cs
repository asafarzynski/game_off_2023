using GameOff2023.Scripts.GameplayCore.Spells;
using GameOff2023.Scripts.Resources;
using GameOff2023.Scripts.Utils;
using Godot;

namespace GameOff2023.Scripts.GameStateManagement;

public partial class GlobalGameData : NodeSingleton<GlobalGameData>
{
	[Export] public SpellsList SpellsList;
	[Export] public EnemiesList EnemiesList;

	public GameplayCore.GameplayCore Core { get; private set; }

	/// <summary>
	/// Add some params, like selected character, etc. in the next step
	/// </summary>
	public void SetUpNewCore()
	{
		Core = new GameplayCore.GameplayCore(SpellsList.GetList(), EnemiesList.GetList());
	}

	public void ClearCore()
	{
		Core = null;
	}
}

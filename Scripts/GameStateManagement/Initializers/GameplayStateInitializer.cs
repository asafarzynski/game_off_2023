using Godot;

namespace GameOff2023.Scripts.GameStateManagement.Initializers;

public partial class GameplayStateInitializer : GameStateInitializer
{
    protected override void OnAfterInitialSet()
    {
        base.OnAfterInitialSet();
        GlobalGameData.Instance.SetUpNewCore();
    }
}
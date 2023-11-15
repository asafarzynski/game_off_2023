namespace GameOff2023.Scripts.GameStateManagement.Initializers;

public partial class GameplayStateInitializer : GameStateInitializer
{
    // Here may go some data, that can be set up from the editor,
    // and then injected into the core,
    // so that we can start gameplay scene with different data in the core.
    // This should be done to test gameplay much quicker.
    // For example - instead of opening the game and going through 7 levels to test the 8th,
    // we can set up "start from level 8" here

    protected override void OnBeforeInitialSet()
    {
        base.OnBeforeInitialSet();
        GlobalGameData.Instance.SetUpNewCore();
    }
}

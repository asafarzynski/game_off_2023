using Godot;

namespace GameOff2023.Scripts.UI;

public partial class UICharacterSelect : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    private void _on_exit_button_pressed()
    {
        // GameStateManager.Instance.ChangeState<MainMenuState>();
    }

    private void _on_start_button_pressed()
    {
        // GameStateManager.Instance.ChangeState<GameplayState>();
    }
}

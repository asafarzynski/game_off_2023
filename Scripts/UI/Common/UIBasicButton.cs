using GameOff2023.Scripts.Audio;
using Godot;

namespace GameOff2023.Scripts.UI.Common;

public partial class UIBasicButton : Button
{
    public virtual void _on_pressed()
    {
        AudioManager.Instance.PlayUISound(UISound.Pressed);
    }

    public virtual void _on_mouse_entered()
    {
        AudioManager.Instance.PlayUISound(UISound.MouseEnter);
    }

    public virtual void _on_mouse_exited()
    {
        // AudioManager.Instance.PlayUISound(UISound.MouseExit);
    }
}

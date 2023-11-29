using GameOff2023.Scripts.Utils;
using Godot;

namespace GameOff2023.Scripts.UI.Battle;

public partial class UIHealthBar : Control
{
    private LazyNode<Label> _label;
    private LazyNode<ProgressBar> _progressBar;

    public UIHealthBar() : base()
    {
        _label = new LazyNode<Label>(() => GetNode<Label>("VBoxContainer/Label"));
        _progressBar = new LazyNode<ProgressBar>(() => GetNode<ProgressBar>("VBoxContainer/ProgressBar"));
    }

    public void UpdateValue(int value)
    {
        _progressBar.Node.Value = value;
    }

    public void SetLabel(string name)
    {
        _label.Node.Text = name;
    }
}

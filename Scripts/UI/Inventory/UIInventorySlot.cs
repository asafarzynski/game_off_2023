using System;
using GameOff2023.Scripts.UI.Common;
using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UIInventorySlot : UIBasicButton
{
    [Export] public TextureRect itemIcon;

    public event Action<int> OnSlotSelected;

    private int _index;

    public void Initialize(int index)
    {
        _index = index;
    }

    public void SetItemImage(Texture2D texture)
    {
        itemIcon.Texture = texture;
    }

    public override void _on_pressed()
    {
        base._on_pressed();
        OnSlotSelected?.Invoke(_index);
    }
}

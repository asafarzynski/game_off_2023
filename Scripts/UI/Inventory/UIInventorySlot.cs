using System;
using GameOff2023.Scripts.UI.Common;
using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UIInventorySlot : UIBasicButton
{
    [Export] private TextureRect itemIcon;

    public event Action<int> OnSlotSelected;

    private int _index;

    public void Initialize(int index)
    {
        _index = index;
    }

    public void SetUp(UIInventorySlotData data)
    {
        if (data == null)
        {
            itemIcon.Texture = null;
            TooltipText = null;
            return;
        }
        
        itemIcon.Texture = data.Icon;
        TooltipText = $"{data.Name}\n\n{data.Description}";
    }

    public override void _on_pressed()
    {
        base._on_pressed();
        OnSlotSelected?.Invoke(_index);
    }

    public class UIInventorySlotData
    {
        public Texture2D Icon;
        public string Name;
        public string Description;
    }
}

using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UISpellSlot : Control
{
    [Export] private UIInventorySlot mainSlot;
    [Export] private UIInventorySlot[] modifierSlots;
}
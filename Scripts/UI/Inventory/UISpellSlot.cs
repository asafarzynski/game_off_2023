using System;
using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UISpellSlot : Control
{
    [Export] private UIInventorySlot mainSlot;
    [Export] private UIInventorySlot[] modifierSlots;
    
    public event Action<int, int> OnSlotSelected;

    private int _mainIndex;

    public void Initialize(int index)
    {
        _mainIndex = index;
        
        mainSlot.Initialize(_mainIndex);
        mainSlot.OnSlotSelected += MainSlotSelected;
        for (var i = 0; i < modifierSlots.Length; i++)
        {
            modifierSlots[i].Initialize(i);
            modifierSlots[i].OnSlotSelected += ModifierSlotSelected;
        }

        UpdateSlots(null,
            new UIInventorySlot.UIInventorySlotData[]
            {
                null,
                null,
                null,
            });
    }

    public void Deinitialize()
    {
        mainSlot.OnSlotSelected -= MainSlotSelected;
        for (var i = 0; i < modifierSlots.Length; i++)
        {
            modifierSlots[i].OnSlotSelected -= ModifierSlotSelected;
        }
    }

    public void UpdateSlots(UIInventorySlot.UIInventorySlotData mainIcon, UIInventorySlot.UIInventorySlotData[] modifiersIcons)
    {
        mainSlot.SetUp(mainIcon);
        for (var i = 0; i < modifierSlots.Length; i++)
        {
            modifierSlots[i].SetUp(modifiersIcons[i]);
        }
    }

    public void SelectMainSlot(bool selected)
    {
        mainSlot.ButtonPressed = selected;
    }

    public void SelectModifierSlot(int index)
    {
        for (var i = 0; i < modifierSlots.Length; i++)
        {
            modifierSlots[i].ButtonPressed = i == index;
        }
    }

    private void MainSlotSelected(int mainIndex)
    {
        OnSlotSelected?.Invoke(mainIndex, -1);
    }

    private void ModifierSlotSelected(int modifierIndex)
    {
        OnSlotSelected?.Invoke(_mainIndex, modifierIndex);
    }
}

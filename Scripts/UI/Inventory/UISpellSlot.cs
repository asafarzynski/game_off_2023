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
            new Texture2D[]
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

    public void UpdateSlots(Texture2D mainIcon, Texture2D[] modifiersIcons)
    {
        mainSlot.SetItemImage(mainIcon);
        for (var i = 0; i < modifierSlots.Length; i++)
        {
            modifierSlots[i].SetItemImage(modifiersIcons[i]);
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

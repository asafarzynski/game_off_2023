using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Inventory;
using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UILooseInventory : Control
{
    [Export] private PackedScene slotScene;
    [Export] private GridContainer gridContainer;

    private int? _selectedSlot;

    private IInventoryItem[] _itemsArray;

    private List<UIInventorySlot> _uiSlots = new();
    private Func<ResourceId, Texture2D> _iconsProviders;

    public void Initialize(IInventoryItem[] itemsArray, Func<ResourceId, Texture2D> iconsProviders)
    {
        _itemsArray = itemsArray;
        _iconsProviders = iconsProviders;
        
        for (var i = 0; i < _itemsArray.Length; i++)
        {
            var newSlot = slotScene.Instantiate();
            gridContainer.AddChild(newSlot);
			
            var uiSlot = newSlot.GetNode<UIInventorySlot>("./");
            uiSlot.Initialize(i);
            uiSlot.SetItemImage(null);
            uiSlot.OnSlotSelected += SelectInventorySlot;
            _uiSlots.Add(uiSlot);
        }
    }

    public void Deinitialize()
    {
        _itemsArray = null;
        foreach (UIInventorySlot uiInventorySlot in _uiSlots)
        {
            uiInventorySlot.OnSlotSelected -= SelectInventorySlot;
            uiInventorySlot.Free();
        }
        _uiSlots.Clear();
    }

    public void UpdateSlots()
    {
        for (var i = 0; i < _uiSlots.Count; i++)
        {
            var coreSlot = _itemsArray[i];
            if (coreSlot != null)
            {
                var icon = _iconsProviders(coreSlot.ResourceId);
                _uiSlots[i].SetItemImage(icon);
            }
            else
            {
                _uiSlots[i].SetItemImage(null);
            }
        }
    }

    private void SelectInventorySlot(int slotIndex)
    {
        _selectedSlot = _selectedSlot == slotIndex ? null : slotIndex;
        for (var i = 0; i < _uiSlots.Count; i++)
        {
            _uiSlots[i].ButtonPressed = i == _selectedSlot;
        }
    }
}
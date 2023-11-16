using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Inventory;
using Godot;

namespace GameOff2023.Scripts.UI.Inventory;

public partial class UISpellSlots : Control
{
    [Export] private PackedScene spellSlotScene;
    [Export] private Control container;

    public event Action<int, int> OnSlotSelected;

    private SpellSlot[] _spellSlots;

    private readonly List<UISpellSlot> _uiSlots = new();
    private Func<ResourceId, Texture2D> _getIcon;

    public void Initialize(SpellSlot[] spellSlots, Func<ResourceId, Texture2D> iconsProviderFunc)
    {
        _spellSlots = spellSlots;

        for (var i = 0; i < _spellSlots.Length; i++)
        {
            var newSlot = spellSlotScene.Instantiate();
            AddChild(newSlot);

            var uiSlot = newSlot.GetNode<UISpellSlot>("./");
            uiSlot.Initialize(i);
            uiSlot.OnSlotSelected += SlotSelected;
            _uiSlots.Add(uiSlot);
        }
        
        _getIcon = iconsProviderFunc;
    }

    public void Deinitialize()
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.OnSlotSelected -= SlotSelected;
            uiSlot.Deinitialize();
            uiSlot.QueueFree();
        }
        _uiSlots.Clear();

        _getIcon = null;
    }

    public void UpdateSlots()
    {
        for (var i = 0; i < _spellSlots.Length; i++)
        {
            if (_spellSlots[i] == null)
                continue;

            Texture2D mainSlotIcon = _spellSlots[i].Spell.HasValue ? _getIcon(_spellSlots[i].Spell.Value.ResourceId) : null;
            Texture2D[] modifierIcons = new Texture2D[3];
            for (var j = 0; j < modifierIcons.Length; j++)
            {
                modifierIcons[j] = _spellSlots[i].Modifiers[j] == null ? null : _getIcon(_spellSlots[i].Modifiers[j].ResourceId);
            }
            _uiSlots[i].UpdateSlots(mainSlotIcon, modifierIcons);
        }
    }

    public void SelectSlot(int mainSlot, int modifierSlot)
    {
        for (var i = 0; i < _uiSlots.Count; i++)
        {
            _uiSlots[i].SelectMainSlot(mainSlot == i && modifierSlot == -1);
            _uiSlots[i].SelectModifierSlot(mainSlot == i ? modifierSlot : -1);
        }
    }

    private void SlotSelected(int mainSlot, int modifierSlot)
    {
        OnSlotSelected?.Invoke(mainSlot, modifierSlot);
    }
}

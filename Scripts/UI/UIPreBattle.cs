using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using GameOff2023.Scripts.GameStateManagement.GameStates.Gameplay;
using GameOff2023.Scripts.UI.Inventory;
using Godot;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPreBattle : UIGameStateSpecific<GameplayState>
{
    // show spell selection UI here
    [Export] private UISpellSlots spellSlots;
    [Export] private UILooseInventory looseInventory;

    private GameplayCore.GameplayCore _core;


    public override void _Ready()
    {
        base._Ready();
        _core = GlobalGameData.Instance.Core;

        looseInventory.Initialize(_core.Inventory.LooseSlots, GetIcon);
        looseInventory.UpdateSlots();
        
        spellSlots.Initialize(_core.Inventory.SpellSlots, GetIcon);
        spellSlots.UpdateSlots();
        spellSlots.OnSlotSelected += SpellSlotSelected;

        _core.Events.InventoryChanged += looseInventory.UpdateSlots;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        _core.Events.InventoryChanged -= looseInventory.UpdateSlots;
        
        spellSlots.OnSlotSelected -= SpellSlotSelected;
        spellSlots.Deinitialize();

        looseInventory.Deinitialize();
    }

    public void _on_confirm_pressed()
    {
        State.InnerStateMachine.Trigger(GameplayTrigger.BattleStarted);
    }

    public void _on_exit_pressed()
    {
        GameStateManager.Instance.StateMachine.Trigger(Triggers.GameEnded);
    }

    private Texture2D
        GetIcon(ResourceId resourceId) // TODO: move to a generic place where more UIs can use it; and utilize IIconContainer
    {
        if (GlobalGameData.Instance.SpellsList.ResourcesDictionary.TryGetValue(resourceId, out var container))
        {
            return container.GetIcon();
        }

        return null;
    }

    private void SpellSlotSelected(int mainSlotIndex, int modifierIndex)
    {
        GD.Print($"Spell slot selected {mainSlotIndex} - {modifierIndex}");
    }
}

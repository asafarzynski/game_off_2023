using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.GameplayCore.Commands;
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
    [Export] private UILooseInventory looseInventory;
    [Export] private UISpellSlots spellSlots;
    [Export] private UIShowEnemies showEnemies;

    private GameplayCore.GameplayCore _core;


    public override void _Ready()
    {
        base._Ready();
        _core = GlobalGameData.Instance.Core;

        showEnemies.Initialize(_core.LevelManager);
        showEnemies.UpdateLabels();
        
        looseInventory.Initialize(_core.Inventory.LooseSlots, GetIcon);
        looseInventory.UpdateSlots();
        looseInventory.OnSlotSelected += InventorySlotSelected;

        spellSlots.Initialize(_core.Inventory.SpellSlots, GetIcon);
        spellSlots.UpdateSlots();
        spellSlots.OnSlotSelected += SpellSlotSelected;

        _core.Events.OnInventoryChanged += UpdateSlots;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        _core.Events.OnInventoryChanged -= UpdateSlots;

        spellSlots.OnSlotSelected -= SpellSlotSelected;
        spellSlots.Deinitialize();

        looseInventory.OnSlotSelected -= InventorySlotSelected;
        looseInventory.Deinitialize();
    }

    public void _on_confirm_pressed()
    {
       var validation = _core.CommandsExecutioner.Do(new FightNextBattleCommand(_core));
       if (!validation.IsValid)
       {
           GD.Print(validation.Message);
       }
       else
       {
           State.InnerStateMachine.Trigger(GameplayTrigger.BattleStarted);
       }
    }

    public void _on_exit_pressed()
    {
        GameStateManager.Instance.StateMachine.Trigger(Triggers.GameEnded);
    }

    private void UpdateSlots()
    {
        spellSlots.UpdateSlots();
        looseInventory.UpdateSlots();
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
        if (_looseInventoryItemSelected != -1)
        {
            var validation = _core.CommandsExecutioner.Do(new MoveFromInventoryToSpellSlot(_core,
                _looseInventoryItemSelected,
                mainSlotIndex,
                modifierIndex == -1 ? null : _spellSlotModifierSelected));

            if (!validation.IsValid)
            {
                GD.Print($"{validation.Message}");
            }
            else
            {
                DeselectAll();
            }
        }
        else
        {
            if (_spellSlotSelected == mainSlotIndex && _spellSlotModifierSelected == modifierIndex)
            {
                DeselectAll();
            }
            else
            {
                spellSlots.SelectSlot(mainSlotIndex, modifierIndex);

                _spellSlotSelected = mainSlotIndex;
                _spellSlotModifierSelected = modifierIndex;
            }
        }
    }

    private void InventorySlotSelected(int index)
    {
        if (_spellSlotSelected != -1)
        {
            var validation = _core.CommandsExecutioner.Do(new MoveFromInventoryToSpellSlot(_core,
                index,
                _spellSlotSelected,
                _spellSlotModifierSelected == -1 ? null : _spellSlotModifierSelected));

            if (!validation.IsValid)
            {
                GD.Print($"{validation.Message}");
            }
            else
            {
                DeselectAll();
            }
        }
        else
        {
            if (_looseInventoryItemSelected == index)
            {
                DeselectAll();
            }
            else
            {
                looseInventory.SelectInventorySlot(index);

                _looseInventoryItemSelected = index;
            }
        }
    }

    private void DeselectAll()
    {
        spellSlots.SelectSlot(-1, -1);
        _spellSlotSelected = -1;
        _spellSlotModifierSelected = -1;

        looseInventory.SelectInventorySlot(-1);
        _looseInventoryItemSelected = -1;
    }

    private int _looseInventoryItemSelected = -1;
    private int _spellSlotSelected = -1;
    private int _spellSlotModifierSelected = -1;
}

using System.Collections.Generic;
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
	[Export] private PackedScene slotScene;
	[Export] private GridContainer gridContainer;

	private List<UIInventorySlot> _slots = new();

	private int? _selectedSlot;
	
	private GameplayCore.GameplayCore _core;
	

	public override void _Ready()
	{
		base._Ready();
		_core = GlobalGameData.Instance.Core;
		for (var i = 0; i < _core.Inventory.LooseSlots.Length; i++)
		{
			var newSlot = slotScene.Instantiate();
			gridContainer.AddChild(newSlot);
			
			var uiSlot = newSlot.GetNode<UIInventorySlot>("./");
			uiSlot.Initialize(i);
			uiSlot.SetItemImage(null);
			uiSlot.OnSlotSelected += SelectInventorySlot;
			_slots.Add(uiSlot);
		}

		_core.Events.InventoryChanged += FillSlotsWithProperData;
		FillSlotsWithProperData();
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		foreach (UIInventorySlot uiInventorySlot in _slots)
		{
			uiInventorySlot.OnSlotSelected -= SelectInventorySlot;
			uiInventorySlot.Free();
		}
		_slots.Clear();
	}

	public void _on_confirm_pressed()
	{
		State.InnerStateMachine.Trigger(GameplayTrigger.BattleStarted);
	}
	
	public void _on_exit_pressed()
	{
		GameStateManager.Instance.StateMachine.Trigger(Triggers.GameEnded);
	}

	private void SelectInventorySlot(int slotIndex)
	{
		_selectedSlot = _selectedSlot == slotIndex ? null : slotIndex;
		for (var i = 0; i < _slots.Count; i++)
		{
			_slots[i].ButtonPressed = i == _selectedSlot;
		}
	}

	private void FillSlotsWithProperData()
	{
		for (var i = 0; i < _slots.Count; i++)
		{
			var coreSlot = _core.Inventory.LooseSlots[i];
			if (coreSlot != null && GlobalGameData.Instance.SpellsList.ResourcesDictionary.TryGetValue(coreSlot.ResourceId, out var resource))
			{
				_slots[i].SetItemImage(resource.Icon);
			}
			else
			{
				_slots[i].SetItemImage(null);
			}
		}
	}
}

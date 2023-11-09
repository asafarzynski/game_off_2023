using GameOff2023.Scripts.GameStateManagement.GameStates.MainMenu;
using GameOff2023.Scripts.Utils.FSM;

namespace GameOff2023.Scripts.GameStateManagement.GameStates;

public class MainMenuState : GameStateManagement.GameState
{
	internal override string SceneName { get; } = "main_menu";

	public FSM<MenuState, MenuTrigger, MainMenuSubState> InnerStateMachine { get; private set; }

	public MainMenuState(GameState id) : base(id)
	{
		InnerStateMachine = new();

		var mainState = new MainSubState(null, MenuState.MainMenu); // fill parent (how?)
		InnerStateMachine.AddState(mainState);
		var creditsState = new CreditsSubState(null, MenuState.Credits); // fill parent (how?)
		InnerStateMachine.AddState(creditsState);
		var settingsState = new SettingsSubState(null, MenuState.Settings); // fill parent (how?)
		InnerStateMachine.AddState(settingsState);
		
		InnerStateMachine.AddTransition(mainState.Id, creditsState.Id, MenuTrigger.CreditsOpened);
		InnerStateMachine.AddTransition(creditsState.Id, mainState.Id, MenuTrigger.CreditsClosed);
		
		InnerStateMachine.AddTransition(mainState.Id, settingsState.Id, MenuTrigger.SettingsOpened);
		InnerStateMachine.AddTransition(settingsState.Id, mainState.Id, MenuTrigger.SettingsClosed);

		InnerStateMachine.TrySetInitialState(MenuState.MainMenu);
	}
}

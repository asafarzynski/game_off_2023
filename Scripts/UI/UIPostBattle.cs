using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using Godot;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPostBattle : UIGameStateSpecific<GameplayState>
{
    public override void _Ready()
    {
        base._Ready();
        GetNode<Button>("%Next").Text = GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightStatus == FightStatus.Win ? "Next battle": "Go to main menu";
    }

    public void _on_next_pressed()
    {
        if (GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightStatus == FightStatus.Win)
        {
            GlobalGameData.Instance.Core.CommandsExecutioner.Do(new GoToNextBattleCommand(GlobalGameData.Instance.Core));
        }
        else
        {
            GameStateManager.Instance.StateMachine.Trigger(Triggers.GameEnded);
        }
    }
}

using GameOff2023.Scripts.GameplayCore.Commands;
using GameOff2023.Scripts.GameplayCore.Levels;
using GameOff2023.Scripts.GameStateManagement;
using GameOff2023.Scripts.GameStateManagement.GameStates;
using Godot;
using GameplayState = GameOff2023.Scripts.GameStateManagement.GameStates.GameplayState;

namespace GameOff2023.Scripts.UI;

public partial class UIPostBattle : UIGameStateSpecific<GameplayState>
{
    private Button _bossButton;

    public override void _Ready()
    {
        base._Ready();
        GetNode<Button>("%Next").Text = GlobalGameData.Instance.Core.LevelManager.CurrentFight.FightStatus == FightStatus.Win ? "Next battle" : "Go to main menu";
        _bossButton = GetNode<Button>("%Boss");
        _bossButton.Visible = GlobalGameData.Instance.Core.LevelManager.CurrentLevel.IsCleared && GlobalGameData.Instance.Core.LevelManager.ReadyForBoss;
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

    public void _on_boss_pressed()
    {
        GlobalGameData.Instance.Core.CommandsExecutioner.Do(new GoToBossBattleCommand(GlobalGameData.Instance.Core));
    }
}

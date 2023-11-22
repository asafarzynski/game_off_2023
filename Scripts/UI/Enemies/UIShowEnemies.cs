using Godot;
using GameOff2023.Scripts.GameplayCore.Levels;

public partial class UIShowEnemies : Control
{
    [Export] private Label[] _enemyLabel;
    private LevelManager _levelManager { get; set; }

    public void Initialize(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void UpdateLabels()
    {
        for (var i = 0; i < 4; i++)
        {
            if (_levelManager.CurrentFight.EnemyList[i] != null)
            {
                _enemyLabel[i].Text = _levelManager.CurrentFight.EnemyList[i].Name;
            }
        }
    }
}

using Godot;
using GameOff2023.Scripts.GameplayCore.Levels;

public partial class UIShowEnemies : Panel
{
    [Export] private Label[] _enemyLabel;
    private LevelManager _levelManager { get; set; }

    public void Initialize(LevelManager levelManager)
    {
        _levelManager = levelManager;
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_levelManager.levels[0].FightList[0].EnemyList[i] != null)
            {
                _enemyLabel[i].Text = _levelManager.levels[0].FightList[0].EnemyList[i].Value.Name;
            }
        }
    }
}

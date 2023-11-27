using System;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCoreEvents
{
    public Action OnInventoryChanged;
    public Action OnNewBattleAhead;
    public Action OnBattleStarted;
    public Action OnBattleEnded;

    public Action OnLevelCleared;
    public Action OnLevelChanged;
    public Action OnGameOver;
}
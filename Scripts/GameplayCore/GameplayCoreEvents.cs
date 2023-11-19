using System;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCoreEvents
{
    public Action OnInventoryChanged;
    public Action OnBattleStarted;

    public Action OnLevelCleared;
    public Action OnLevelChanged;
}
using GameOff2023.Scripts.Commands;

namespace GameOff2023.Scripts.GameplayCore;

public class GameplayCore
{
    public readonly CommandsExecutioner CommandsExecutioner = new();
    public float Score { get; internal set; } = 1f;
}
using Godot;

namespace GameOff2023.Scripts.Characters;

public static class AnimationTreeHelper
{
    /// <summary>
    /// Use it to set a condition stored with a given name without a need to manually set a path to it.
    /// </summary>
    /// <param name="animationTree">Tree that you want to change</param>
    /// <param name="conditionName">Named set in AnimationNodeStateMachineTransition</param>
    /// <param name="boolValue">New value</param>
    /// <returns></returns>
    public static void SetCondition(this AnimationTree animationTree, string conditionName, bool boolValue)
    {
        animationTree.Set($"parameters/conditions/{conditionName}", boolValue);
    }
}

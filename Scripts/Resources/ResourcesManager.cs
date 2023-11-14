using GameOff2023.Scripts.GameplayCore;
using Godot.Collections;

namespace GameOff2023.Scripts.Resources;

public static class ResourcesManager
{
    private static readonly Dictionary<string, int> NextIdsDictionary = new();

    public static ResourceId GetId(string baseName)
    {
        if (!NextIdsDictionary.ContainsKey(baseName))
        {
            NextIdsDictionary.Add(baseName, 0);
        }
        
        return new ResourceId(baseName, NextIdsDictionary[baseName]++);
    }
}
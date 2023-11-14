using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class ResourcesList<T> : Resource
    where T : Resource
{
    [Export] private string baseName;
    [Export] private Resource[] resources;

    public readonly Dictionary<ResourceId, T> ResourcesDictionary = new();

    public void PrepareDictionary()
    {
        foreach (Resource resource in resources)
        {
            if (resource is T typedResource)
            {
                ResourcesDictionary.Add(ResourcesManager.GetId(baseName), typedResource);
            }
        }
    }
}

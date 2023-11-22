using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using GameOff2023.Scripts.Resources.Interfaces;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class ResourcesList<T> : Resource
    where T : Resource
{
    [Export] private string baseName;
    [Export] private Resource[] resources;

    public Dictionary<ResourceId, T> ResourcesDictionary;

    public void PrepareDictionary()
    {
        if (ResourcesDictionary != null)
            return;

        ResourcesDictionary = new();

        foreach (Resource resource in resources)
        {
            if (resource is T typedResource)
            {
                if (ResourcesManager.TryRegister(baseName, typedResource, out var id))
                {
                    ResourcesDictionary.Add(id, typedResource);
                    if (typedResource is IInnerResourceLists innerResourceList)
                    {
                        innerResourceList.PrepareDictionaries();
                    }
                }
            }
        }
    }
}

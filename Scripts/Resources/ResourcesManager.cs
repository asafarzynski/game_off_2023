using System;
using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using Godot;

namespace GameOff2023.Scripts.Resources;

/// <summary>
/// After a resource is registered using <see cref="TryRegister{T}"/> it is available using <see cref="GetResource{T}"/>.
/// <br/>
/// <br/>
/// You just need to be sure about the type of the resource, but it can be checked with <see cref="GetResourceType"/>.
/// </summary>
public static class ResourcesManager
{
    private static readonly HashSet<Resource> Resources = new();
    private static readonly Dictionary<ResourceId, Resource> ResourcesDictionary = new();
    private static readonly Dictionary<string, Type> ResourcesTypes = new();
    private static readonly Dictionary<string, int> NextIdsDictionary = new();

    public static bool TryRegister<T>(string baseName, T resource, out ResourceId resourceId) where T : Resource
    {
        resourceId = default;
        if (Resources.Contains(resource))
            return false; // we may try to register multiple times in some cases of defensive programming

        var resourceType = resource.GetType();

        if (ResourcesTypes.TryGetValue(baseName, out var previousResourceType))
        {
            if (previousResourceType != resourceType) // we should never try to register multiple different times with the same name
                throw new Exception("Trying to register multiple different types under one base name!");
        }
        else
        {
            ResourcesTypes.Add(baseName, resourceType);
        }

        if (!NextIdsDictionary.ContainsKey(baseName))
        {
            NextIdsDictionary.Add(baseName, 0);
        }

        resourceId = new ResourceId(baseName, NextIdsDictionary[baseName]++);
        ResourcesDictionary.Add(resourceId, resource);
        return true;
    }

    public static T GetResource<T>(ResourceId resourceId) where T : Resource
    {
        if (!ResourcesDictionary.TryGetValue(resourceId, out var resource))
            return null;

        return (T)resource;
    }

    public static Type GetResourceType(ResourceId resourceId)
    {
        if (!ResourcesTypes.TryGetValue(resourceId.BaseName, out var type))
            return null;

        return type;
    }
}

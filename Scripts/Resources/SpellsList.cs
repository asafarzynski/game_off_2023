using System.Collections.Generic;
using GameOff2023.Scripts.GameplayCore;
using Godot;

namespace GameOff2023.Scripts.Resources;

public partial class SpellsList : Resource
{
	[Export] private SpellResource[] Spells;

	public readonly Dictionary<ResourceId, SpellResource> ResourcesDictionary = new();

	public void PrepareDictionary()
	{
		foreach (SpellResource spellResource in Spells)
		{
			ResourcesDictionary.Add(ResourcesManager.GetId("Spell"), spellResource);
		}
	}
}

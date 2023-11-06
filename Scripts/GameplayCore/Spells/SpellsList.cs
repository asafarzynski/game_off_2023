using System.Collections.Generic;
using Godot;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public partial class SpellsList : Resource
{
    [Export] public Resource[] Spells;

    public List<Spell> GetList()
    {
        var list = new List<Spell>();
        foreach (Resource resource in Spells)
        {
            if (resource is SpellResource spellResource)
            {
                list.Add(spellResource.ToSpell());
            }
        }
        return list;
    }
}
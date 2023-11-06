using Godot;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public partial class SpellResource : Resource
{
    [Export] public float Cooldown;
    [Export] public int Count;
    [Export] public float Damage;
    [Export] public float CriticalChance;

    public Spell ToSpell() => new Spell()
    {
        Cooldown = Cooldown,
        Count = Count,
        Damage = Damage,
        CriticalChance = CriticalChance,
    };
}
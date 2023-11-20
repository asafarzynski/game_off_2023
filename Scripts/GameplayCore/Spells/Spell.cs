using GameOff2023.Scripts.GameplayCore.Inventory;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public struct Spell : IInventoryItem
{
    public ResourceId ResourceId { get; }

    public int Cooldown; // Multiples of 2: 1, 2, 4, 8, 16, etc.
    public int Count;
    public float Damage;
    public float CriticalChance;
    public SpellTarget Target { get; set; }

    public Spell(ResourceId resourceId) : this(resourceId, 1, 1, 1f, 0.1f, SpellTarget.FirstEnemy) { }

    public Spell(ResourceId resourceId, int cooldown, int count, float damage, float criticalChance, SpellTarget target)
    {
        ResourceId = resourceId;
        Cooldown = cooldown;
        Count = count;
        Damage = damage;
        CriticalChance = criticalChance;
        Target = target;
    }

    public override string ToString()
    {
        return $"[Cooldown: {Cooldown} | Count: {Count} | Damage: {Damage} | Critical chance: {CriticalChance}]";
    }
}

using GameOff2023.Scripts.GameplayCore.Inventory;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public struct Spell : IInventoryItem
{
    public ResourceId ResourceId { get; }

    public float Cooldown;
    public int Count;
    public float Damage;
    public float CriticalChance;

    public Spell(ResourceId resourceId) : this(resourceId, 1f, 1, 1f, 0.1f) { }

    public Spell(ResourceId resourceId, float cooldown, int count, float damage, float criticalChance)
    {
        ResourceId = resourceId;
        Cooldown = cooldown;
        Count = count;
        Damage = damage;
        CriticalChance = criticalChance;
    }

    public override string ToString()
    {
        return $"[Cooldown: {Cooldown} | Count: {Count} | Damage: {Damage} | Critical chance: {CriticalChance}]";
    }
}

namespace GameOff2023.Scripts.GameplayCore.Spells;

public struct Spell
{
    public float Cooldown;
    public int Count;
    public float Damage;
    public float CriticalChance;

    public Spell()
        : this(1f, 1, 1f, 0.1f) { }

    public Spell(float cooldown, int count, float damage, float criticalChance)
    {
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

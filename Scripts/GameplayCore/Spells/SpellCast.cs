using GameOff2023.Scripts.GameplayCore.Levels;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public class SpellCast
{
    public Spell Spell { get; set; }
    public int Cooldown { get; set; }
    public FightingCharacter OriginCharacter { get; set; }
}

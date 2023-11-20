using GameOff2023.Scripts.GameplayCore.Characters;
namespace GameOff2023.Scripts.GameplayCore.Spells;

public class SpellCast
{
    public Spell Spell { get; set; }
    public int Cooldown { get; set; }
    public Character OriginCharacter { get; set; }
}

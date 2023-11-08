using System.Collections.Generic;
using System.Text;

namespace GameOff2023.Scripts.GameplayCore.Spells;

public class SpellStack
{
	private readonly Dictionary<float, List<Spell>> _stack = new Dictionary<float, List<Spell>>();
	
	public override string ToString()
	{
		StringBuilder sb = new();

		foreach (var kvp in _stack)
		{
			sb.AppendLine($"[{kvp.Key}]");
			sb.AppendLine(string.Join(", \n", kvp.Value));
			sb.AppendLine();
		}

		return sb.ToString();
	}
}

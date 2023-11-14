using System.Collections.Generic;
using Godot;
using GameOff2023.Scripts.GameplayCore.Enemies;
namespace GameOff2023.Scripts.Resources;

public partial class EnemiesList : Resource
{
	[Export] public EnemyResource[] Enemies;

	public List<Enemy> GetList()
	{
		var list = new List<Enemy>();
		foreach (Resource resource in Enemies)
		{
			if (resource is EnemyResource enemyResource)
			{
				list.Add(enemyResource.ToEnemy(ResourcesManager.GetId("Enemy")));
			}
		}
		return list;
	}
}

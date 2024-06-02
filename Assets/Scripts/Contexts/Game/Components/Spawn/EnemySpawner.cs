using Configs.Enemies;
using Contexts.Game.Factories;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public class EnemySpawner : Spawner
	{
		public override GameObject Spawn()
		{
			EnemiesCreator creator = new EnemiesCreator();
			EnemyConfig enemyConfig = GetRandomConfig() as EnemyConfig;
			Enemy.Enemy enemy = creator.Create(enemyConfig, transform);
			enemy.transform.position = GetRandomSpawnPosition();
			return enemy.gameObject;
		}
	}
}
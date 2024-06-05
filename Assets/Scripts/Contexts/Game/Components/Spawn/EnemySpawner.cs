using Configs;
using Configs.Enemies;
using Contexts.Game.Factories;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public class EnemySpawner : Spawner
	{
		public override GameObject Spawn(Config config, Transform parent)
		{
			if (config is not EnemyConfig)
			{
				Debug.LogError($"Config wich you want to spawn is not EnemyConfig. Config: {config.GetType().Name}");
				return null;
			}

			EnemiesCreator creator = new EnemiesCreator();
			EnemyConfig enemyConfig = config as EnemyConfig;
			Enemy.Enemy enemy = creator.Create(enemyConfig, parent);
			enemy.transform.position = GetRandomSpawnPosition();
			return enemy.gameObject;
		}
	}
}
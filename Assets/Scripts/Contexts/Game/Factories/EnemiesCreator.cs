using Configs.Enemies;
using Contexts.Game.Components.Enemy;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public class EnemiesCreator
	{
		public Enemy Create(string id, Vector3 worldPosition)
		{
			EnemiesDataProvider dataProvider = ServicesManager.Instance.Get<EnemiesDataProvider>();
			EnemyConfig config = dataProvider.GetEnemyConfig(id);
			if (config == null)
			{
				Debug.LogError($"Enemy with id {id} doesn't exist");
				return null;
			}

			return Create(config, worldPosition);
		}

		public Enemy Create(string id, Transform parent)
		{
			EnemiesDataProvider dataProvider = ServicesManager.Instance.Get<EnemiesDataProvider>();
			EnemyConfig config = dataProvider.GetEnemyConfig(id);
			if (config == null)
			{
				Debug.LogError($"Enemy with id {id} doesn't exist");
				return null;
			}

			return Create(config, parent);
		}

		public Enemy Create(EnemyConfig config, Vector3 worldPosition)
		{
			Enemy enemy = Create(config, null);
			enemy.transform.position = worldPosition;
			return enemy;
		}

		public Enemy Create(EnemyConfig config, Transform parent)
		{
			if (config == null)
			{
				Debug.LogError($"Enemy with id {config} doesn't exist");
				return null;
			}

			Enemy prefabRef = config.Prefab;
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
			Enemy enemy = pool.Get(parent);
			enemy.Configure(config);
			return enemy;
		}
	}
}
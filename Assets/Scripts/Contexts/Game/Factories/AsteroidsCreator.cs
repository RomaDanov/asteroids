using Architecture.ObjectPool;
using Architecture.ServiceLocator;
using Configs.Asteriods;
using Contexts.Game.Components.Asteroid;
using DataProviders;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public class AsteroidsCreator
	{
		public Asteroid Create(string id, Vector3 worldPosition)
		{
			AsteroidsDataProvider dataProvider = ServicesManager.Instance.Get<AsteroidsDataProvider>();
			AsteroidConfig config = dataProvider.GetAsteroidConfig(id);
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {id} doesn't exist");
				return null;
			}

			return Create(config, worldPosition);
		}

		public Asteroid Create(string id, Transform parent)
		{
			AsteroidsDataProvider dataProvider = ServicesManager.Instance.Get<AsteroidsDataProvider>();
			AsteroidConfig config = dataProvider.GetAsteroidConfig(id);
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {id} doesn't exist");
				return null;
			}

			return Create(config, parent);
		}

		public Asteroid Create(AsteroidConfig config, Vector3 worldPosition)
		{
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {config} doesn't exist");
				return null;
			}

			Asteroid prefabRef = config.Prefab;
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
			Asteroid asteroid = pool.Get();
			asteroid.transform.position = worldPosition;
			asteroid.Configure(config);
			return asteroid;
		}

		public Asteroid Create(AsteroidConfig config, Transform parent)
		{
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {config} doesn't exist");
				return null;
			}

			Asteroid prefabRef = config.Prefab;
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
			Asteroid asteroid = pool.Get(parent);
			asteroid.Configure(config);
			return asteroid;
		}
	}
}
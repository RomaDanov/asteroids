using Configs.Asteriods;
using Contexts.Game.Components.Asteroid;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public class AsteroidsCreator : IAsteroidsCreator
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
	}
}
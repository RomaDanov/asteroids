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
		public Asteroid Create(string id)
		{
			AsteroidsDataProvider dataProvider = ServicesManager.Instance.Get<AsteroidsDataProvider>();
			AsteroidConfig config = dataProvider.GetAsteroidConfig(id);
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {id} doesn't exist");
				return null;
			}

			return Create(config);
		}

		public Asteroid Create(AsteroidConfig config)
		{
			if (config == null)
			{
				Debug.LogError($"Asteroid with id {config} doesn't exist");
				return null;
			}

			Asteroid prefabRef = config.Prefab;
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
			Asteroid asteroid = pool.Get();
			asteroid.Configure(config);
			return asteroid;
		}
	}
}
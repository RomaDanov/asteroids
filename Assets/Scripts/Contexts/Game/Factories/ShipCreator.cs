using Architecture.ObjectPool;
using Architecture.ServiceLocator;
using Configs.Ships;
using DataProviders;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public class ShipCreator
	{
		public ShipView Create(string id, Transform parent)
		{
			ShipsDataProvider shipsDataProvider = ServicesManager.Instance.Get<ShipsDataProvider>();
			ShipConfig config = shipsDataProvider.GetShipConfig(id);
			if (config == null)
			{
				Debug.LogError($"Ship with id {id} doesn't exist");
				return null;
			}

			return Create(config, parent);
		}

		public ShipView Create(ShipConfig config, Transform parent)
		{
			if (config == null)
			{
				Debug.LogError($"Ship with id {config} doesn't exist");
				return null;
			}

			ShipView prefabRef = config.Prefab;
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
			ShipView ship = pool.Get(parent);
			return ship;
		}
	}
}
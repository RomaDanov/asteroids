using Configs.Ships;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;

public class ShipCreator : IShipCreator
{
	public ShipController Create(string id, Transform parent, Transform controllTransform = null)
	{
		ShipsDataProvider shipsDataProvider = ServicesManager.Instance.Get<ShipsDataProvider>();
		ShipConfig config = shipsDataProvider.GetShipConfig(id);
		if (config == null)
		{
			Debug.Log($"Ship with id {id} doesn't exist");
			return null;
		}

		return Create(config, parent, controllTransform);
	}

	public ShipController Create(ShipConfig config, Transform parent, Transform controllTransform = null)
	{
		if (config == null)
		{
			Debug.Log($"Ship with id {config} doesn't exist");
			return null;
		}

		ShipController prefabRef = config.Prefab;
		var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
		ShipController ship = pool.Get(parent);
		ship.Configure(config, controllTransform);
		return ship;
	}
}

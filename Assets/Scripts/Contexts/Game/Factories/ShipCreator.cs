using Configs.Ships;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;

public class ShipCreator : IShipCreator
{
	public ShipView Create(string id, Transform parent, Transform controllTransform = null)
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

	public ShipView Create(ShipConfig config, Transform parent, Transform controllTransform = null)
	{
		if (config == null)
		{
			Debug.Log($"Ship with id {config} doesn't exist");
			return null;
		}

		ShipView prefabRef = config.Prefab;
		var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 5);
		ShipView ship = pool.Get(parent);
		ship.Configure(config.Id);
		return ship;
	}
}

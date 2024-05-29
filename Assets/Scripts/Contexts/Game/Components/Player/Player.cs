using Configs.Ships;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private PlayerMovement movement;

	private ShipView ship;

	public void Configure(ShipConfig shipConfig)
	{
		ConfigureShip(shipConfig);
		movement.Configure(shipConfig.MovementSettings);
	}

	private void ConfigureShip(ShipConfig ship)
	{
		ShipCreator creator = new ShipCreator();
		this.ship = creator.Create(ship, transform, transform);
	}

	private void Update()
	{
		///TEST
		if (Keyboard.current.digit1Key.wasPressedThisFrame && ship.Id != "SHIP_BLUE")
		{
			ShipConfig config = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig(ship.Id);
			var shipPool = ObjectPoolService.Instance.GetOrCreatePool(config.Prefab, 5);
			shipPool.Release(ship);
			ship = null;

			ShipConfig newConfig = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig("SHIP_BLUE");
			ConfigureShip(newConfig);
			movement.Configure(newConfig.MovementSettings);
		}
		else if (Keyboard.current.digit2Key.wasPressedThisFrame && ship.Id != "SHIP_RED")
		{
			ShipConfig config = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig(ship.Id);
			var shipPool = ObjectPoolService.Instance.GetOrCreatePool(config.Prefab, 5);
			shipPool.Release(ship);
			ship = null;

			ShipConfig newConfig = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig("SHIP_RED");
			ConfigureShip(newConfig);
			movement.Configure(newConfig.MovementSettings);
		}
		///
	}
}

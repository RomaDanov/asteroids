using Configs.Ships;
using DataProviders;
using Inputs;
using ObjectPool;
using ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private ShipController shipController;

	public void Configure(ShipConfig ship)
	{
		ConfigureShip(ship);
	}

	private void ConfigureShip(ShipConfig ship)
	{
		ShipCreator creator = new ShipCreator();
		shipController = creator.Create(ship, transform, transform);
	}

	private void Update()
	{
		///TEST
		if (Keyboard.current.digit1Key.wasPressedThisFrame && shipController.Id != "SHIP_BLUE")
		{
			ShipConfig config = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig(shipController.Id);
			var shipPool = ObjectPoolService.Instance.GetOrCreatePool(config.Prefab, 5);
			shipPool.Release(shipController);
			shipController = null;
			ConfigureShip(ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig("SHIP_BLUE"));
		}
		else if (Keyboard.current.digit2Key.wasPressedThisFrame && shipController.Id != "SHIP_RED")
		{
			ShipConfig config = ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig(shipController.Id);
			var shipPool = ObjectPoolService.Instance.GetOrCreatePool(config.Prefab, 5);
			shipPool.Release(shipController);
			shipController = null;
			ConfigureShip(ServicesManager.Instance.Get<ShipsDataProvider>().GetShipConfig("SHIP_RED"));
		}
		///

		IInput input = InputManager.Instance.Input;

		if (shipController != null)
		{
			shipController.ApplyMove(input.GetAccelerate());
			shipController.ApplyRotation(input.GetRotation());
		}
	}
}

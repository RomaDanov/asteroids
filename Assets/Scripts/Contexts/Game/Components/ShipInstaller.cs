using Configs.Ships;
using Contexts.Game.Components.Movements;
using Contexts.Game.Factories;
using UnityEngine;

public class ShipInstaller : MonoBehaviour
{
	[SerializeField] private Transform root;
	[SerializeField] private TransformMovement movable;

	public ShipView Ship { get; private set; }

	public void Install(ShipConfig ship)
	{
		ShipCreator creator = new ShipCreator();
		Ship = creator.Create(ship, root, movable);
		Ship.gameObject.layer = gameObject.layer;
	}

	public void Uninstall()
	{
		if (Ship == null) return;
		Ship.Pool.Release(Ship);
	}
}

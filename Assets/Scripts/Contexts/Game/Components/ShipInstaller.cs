using Configs.Ships;
using Contexts.Game.Factories;
using UnityEngine;

public class ShipInstaller : MonoBehaviour
{
	[SerializeField] private Transform root;

	public ShipView Ship { get; private set; }

	public void Install(ShipConfig ship)
	{
		ShipCreator creator = new ShipCreator();
		Ship = creator.Create(ship, root);
		Ship.gameObject.layer = gameObject.layer;
	}
}

using Configs.Ships;
using Inputs;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Ship shipComponent;

	public void Configure(ShipConfig ship)
	{
		shipComponent.Configure(ship, transform);
	}

	private void Update()
	{
		IInput input = InputManager.Instance.Input;

		shipComponent.MoveDirection = input.GetAccelerate();
		shipComponent.RotateDirection = input.GetRotation();
	}
}

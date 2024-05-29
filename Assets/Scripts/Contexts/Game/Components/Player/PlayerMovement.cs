using Configs.Ships;
using Inputs;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
	private MovementSettings movementSettings;

	private Vector3 inputAxis;
	private float acceleration;

	public float Velocity { get; private set; }

	public void Configure(MovementSettings movementSettings)
	{
		this.movementSettings = movementSettings;
	}

	private void Update()
	{
		IInput input = InputManager.Instance.Input;
		if (input == null) return;

		inputAxis = input.GetAxis();
	}

	private void FixedUpdate()
	{
		Slowdown();
		Move(inputAxis.y);
		Rotate(inputAxis.x);
	}

	private void ForceBreak()
	{
		acceleration = 0;
		Velocity = 0;
	}

	private void Slowdown()
	{
		if (Velocity > 0)
		{
			Velocity -= movementSettings.BrakingSpeed * Time.fixedDeltaTime;
			if (Velocity < 0)
			{
				Velocity = 0;
			}
		}
	}

	private void Move(float direction)
	{
		acceleration += direction * movementSettings.Acceleration;
		Velocity += acceleration;
		Velocity = Velocity < movementSettings.MaxSpeed ? Velocity : movementSettings.MaxSpeed;
		transform.position += transform.up * Velocity * Time.fixedDeltaTime;
		acceleration *= 0;
	}

	private void Rotate(float direction)
	{
		transform.up = Quaternion.AngleAxis(-direction * movementSettings.Torq * Time.fixedDeltaTime, Vector3.forward) * transform.up;
	}
}

using Configs.Ships;
using UnityEngine;

public class Ship : MonoBehaviour
{
	private ShipConfig config;
	private Transform controlTransform;
	private float velocity;
	private float acceleration;

	public float MoveDirection { get; set; }
	public float RotateDirection { get; set; }

	public void Configure(ShipConfig config, Transform controlTransform)
	{
		this.config = config;
		this.controlTransform = controlTransform;
	}

	private void FixedUpdate()
	{
		if (config == null) return;

		Brake();
		Move(Mathf.Clamp(MoveDirection, 0, 1));
		Rotate(RotateDirection);
	}

	public void ForceBreak()
	{
		acceleration = 0;
		velocity = 0;
	}

	private void Brake()
	{
		if (velocity > 0)
		{
			velocity -= config.BrakingSpeed * Time.fixedDeltaTime;
			if (velocity < 0)
			{
				velocity = 0;
			}
		}
	}

	private void Move(float direction)
	{
		acceleration += direction * config.Acceleration;
		velocity += acceleration;
		velocity = velocity < config.MaxSpeed ? velocity : config.MaxSpeed;
		controlTransform.position += controlTransform.up * velocity * Time.fixedDeltaTime;
		acceleration *= 0;
	}

	private void Rotate(float direction)
	{
		controlTransform.up = Quaternion.AngleAxis(-direction * config.Torq * Time.fixedDeltaTime, Vector3.forward) * controlTransform.up;
	}
}

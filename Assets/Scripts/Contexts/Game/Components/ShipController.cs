using Configs.Ships;
using ObjectPool;
using UnityEngine;

public class ShipController : PoolableObject<ShipController>
{
	private ShipConfig config;
	private Transform controlTransform;
	private float acceleration;

	public string Id => config != null ? config.Id : string.Empty;
	public float Velocity { get; private set; }
	public float MoveForce { get; private set; }
	public float RotateDirection { get; private set; }

	public void Configure(ShipConfig config, Transform controlTransform = null)
	{
		this.config = config;
		this.controlTransform = controlTransform != null ? controlTransform : transform;
	}

	private void FixedUpdate()
	{
		if (config == null) return;

		Brake();
		Move(MoveForce);
		Rotate(RotateDirection);
	}

	public void ApplyMove(float force)
	{
		MoveForce = Mathf.Clamp01(force);
	}

	public void ApplyRotation(float direction)
	{
		RotateDirection = direction;
	}

	public void ForceBreak()
	{
		MoveForce = 0;
		RotateDirection = 0;
		acceleration = 0;
		Velocity = 0;
	}

	private void Brake()
	{
		if (Velocity > 0)
		{
			Velocity -= config.BrakingSpeed * Time.fixedDeltaTime;
			if (Velocity < 0)
			{
				Velocity = 0;
			}
		}
	}

	private void Move(float direction)
	{
		acceleration += direction * config.Acceleration;
		Velocity += acceleration;
		Velocity = Velocity < config.MaxSpeed ? Velocity : config.MaxSpeed;
		controlTransform.position += controlTransform.up * Velocity * Time.fixedDeltaTime;
		acceleration *= 0;
	}

	private void Rotate(float direction)
	{
		controlTransform.up = Quaternion.AngleAxis(-direction * config.Torq * Time.fixedDeltaTime, Vector3.forward) * controlTransform.up;
	}

	public override void OnRelease()
	{
		ForceBreak();
		config = null;
	}
}

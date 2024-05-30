using Configs.Weapons;
using Contexts.Game.Components.Movement;
using ObjectPool;
using UnityEngine;

public class Projectile : PoolableObject<Projectile>
{
	[SerializeField] private TransformMovement movable;
	[SerializeField] private TrailRenderer trail;

	private ProjectileStats stats;
	private Vector2 pushDirection;

	public virtual void Configure(ProjectileStats stats, Vector2 pushDirection)
	{
		this.stats = stats;
		this.pushDirection = pushDirection;

		LookAt(pushDirection);
		Push(transform.up * stats.Speed);

		Invoke(nameof(Hide), 2);
	}

	private void LookAt(Vector3 direction)
	{
		transform.up = direction;
	}

	private void Push(Vector3 force)
	{
		movable.Velocity = transform.up * stats.Speed;
	}

	private void SetTrailActive(bool isActive)
	{
		trail.enabled = isActive;
	}

	private void Hide()
	{
		Pool.Release(this);
	}

	public override void OnGet()
	{
		SetTrailActive(true);
	}

	public override void OnRelease()
	{
		movable.Velocity = Vector2.zero;
		SetTrailActive(false);
	}
}

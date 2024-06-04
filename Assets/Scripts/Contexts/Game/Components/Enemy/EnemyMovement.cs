using Contexts.Game.Components.Movements;
using UnityEngine;

namespace Contexts.Game.Components.Enemy
{
	[RequireComponent(typeof(IMovable))]
	public class EnemyMovement : Movement
	{
		[SerializeField] private TransformMovement movable;

		private Transform target;
		private float stoppingDistance;

		public void Configure(float stoppingDistance)
		{
			this.stoppingDistance = stoppingDistance;

			if (target == null)
			{
				Player.Player player = FindObjectOfType<Player.Player>();
				if (player != null)
				{
					target = player.transform;
				}
			}
		}

		private void FixedUpdate()
		{
			ClampVelocity();

			if (target == null)
			{
				Move();
				LookAt(Vector3.up * 100);
				return;
			}

			if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
			{
				Move();
			}
			else
			{
				Slowdown();
			}

			Vector3 targetLookPosition = transform.InverseTransformPoint(target.position);
			LookAt(targetLookPosition);
		}

		public void ForceStop()
		{
			movable.Velocity = Vector2.zero;
		}

		private void Slowdown()
		{
			Vector2 brakeForce = -movable.Velocity * settings.BrakeForce * Time.fixedDeltaTime;
			movable.ApplyForce(brakeForce);

			MoveProccessing?.Invoke(false);
		}

		private void Move()
		{
			Vector2 moveForce = transform.up * settings.Acceleration * Time.fixedDeltaTime;
			movable.ApplyForce(moveForce);

			MoveProccessing?.Invoke(true);
		}

		private void LookAt(Vector3 lookPosition)
		{
			float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg - 90;
			transform.Rotate(0, 0, angle);
		}

		private void ClampVelocity()
		{
			Vector2 clampedVelocity = Vector2.ClampMagnitude(movable.Velocity, settings.MaxSpeed);
			movable.Velocity = clampedVelocity;
		}
	}
}
using Architecture.Inputs;
using Contexts.Game.Components.Movements;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	[RequireComponent(typeof(IMovable))]
	public class PlayerMovement : Movement
	{
		[SerializeField] private TransformMovement movable;

		private Vector3 inputAxis;

		private void Update()
		{
			IInput input = InputManager.Instance.Input;
			if (input == null) return;

			inputAxis = input.GetAxis();
		}

		private void FixedUpdate()
		{
			if (!Mathf.Approximately(inputAxis.y, 0))
			{
				Move();
			}
			else
			{
				Slowdown();
			}

			Rotate();
			ClampVelocity();
		}

		private void Slowdown()
		{
			Vector2 brakeForce = -movable.Velocity * settings.BrakeForce * Time.fixedDeltaTime;
			movable.ApplyForce(brakeForce);

			MoveProccessing?.Invoke(false);
		}

		private void Move()
		{
			float clampedDirection = Mathf.Clamp(inputAxis.y, 0f, 1f);
			Vector2 moveForce = transform.up * clampedDirection * settings.Acceleration * Time.fixedDeltaTime;
			movable.ApplyForce(moveForce);

			MoveProccessing?.Invoke(true);
		}

		private void Rotate()
		{
			transform.rotation = Quaternion.AngleAxis(-inputAxis.x * settings.Torq * Time.fixedDeltaTime, Vector3.forward) * transform.rotation;
		}

		private void ClampVelocity()
		{
			Vector2 clampedVelocity = Vector2.ClampMagnitude(movable.Velocity, settings.MaxSpeed);
			movable.Velocity = clampedVelocity;
		}
	}
}
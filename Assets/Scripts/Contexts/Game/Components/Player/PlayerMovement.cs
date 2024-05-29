using Configs.Ships;
using Contexts.Game.Components.Movement;
using Inputs;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	[RequireComponent(typeof(IMovable))]
	public class PlayerMovement : MonoBehaviour
	{
		private IMovable movable;
		private MovementSettings movementSettings;

		private Vector3 inputAxis;

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
			Move();
			Rotate();
			ClampVelocity();
		}

		private void ForceBreak()
		{
			movable.ApplyForce(-movable.Velocity);
		}

		private void Slowdown()
		{
			if (inputAxis.y > 0) return;

			Vector2 brakeForce = -movable.Velocity * movementSettings.BrakeForce * Time.fixedDeltaTime;
			movable.ApplyForce(brakeForce);
		}

		private void Move()
		{
			float clampedDirection = Mathf.Clamp(inputAxis.y, 0f, 1f);
			Vector2 moveForce = transform.up * clampedDirection * movementSettings.Acceleration * Time.fixedDeltaTime;
			movable.ApplyForce(moveForce);
		}

		private void Rotate()
		{
			transform.up = Quaternion.AngleAxis(-inputAxis.x * movementSettings.Torq * Time.fixedDeltaTime, Vector3.forward) * transform.up;
		}

		private void ClampVelocity()
		{
			Vector2 clampedVelocity = Vector2.ClampMagnitude(movable.Velocity, movementSettings.MaxSpeed);
			movable.Velocity = clampedVelocity;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (movable != null) return;
			movable = GetComponent<IMovable>();
		}
#endif
	}
}
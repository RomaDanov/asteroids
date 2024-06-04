using UnityEngine;

namespace Contexts.Game.Components.Movement
{
	public class TransformMovement : MonoBehaviour, IMovable
	{
		public Vector2 Velocity { get; set; }
		public Vector2 Acceleration { get; private set; }
		public IMovable.AccelerationState State { get; private set; }


		private Vector2 prevAcceleration;

		private void FixedUpdate()
		{
			float current = Acceleration.magnitude;
			float previous = prevAcceleration.magnitude;

			if (current > previous && !Mathf.Approximately(current, previous))
			{
				State = IMovable.AccelerationState.INCREASE;
			}
			else if (current < previous && !Mathf.Approximately(current, previous))
			{
				State = IMovable.AccelerationState.DECREASE;
			}

			prevAcceleration = Acceleration;
			UpdateMovement();
		}

		public void ApplyForce(Vector2 force)
		{
			Acceleration += force;
		}

		private void UpdateMovement()
		{
			Velocity += Acceleration;
			transform.position += (Vector3)Velocity * Time.fixedDeltaTime;
			Acceleration = Vector2.zero;
		}
	}
}
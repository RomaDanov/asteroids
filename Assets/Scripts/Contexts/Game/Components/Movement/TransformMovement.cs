using UnityEngine;

namespace Contexts.Game.Components.Movement
{
	public class TransformMovement : MonoBehaviour, IMovable
	{
		public Vector2 Velocity { get; set; }
		public Vector2 Acceleration { get; private set; }

		private void FixedUpdate()
		{
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
using UnityEngine;

namespace Contexts.Game.Components.Movements
{
	public class TransformMovement : MonoBehaviour, IMovable
	{
		public Vector2 Velocity { get; set; }

		private Vector2 acceleration;

		private void FixedUpdate()
		{
			UpdateMovement();
		}

		public void ApplyForce(Vector2 force)
		{
			acceleration += force;
		}

		private void UpdateMovement()
		{
			Velocity += acceleration;
			transform.position += (Vector3)Velocity * Time.fixedDeltaTime;
			acceleration = Vector2.zero;
		}
	}
}
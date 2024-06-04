using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Movement;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Reflector : MonoBehaviour
	{
		[SerializeField] private TransformMovement movable;
		[SerializeField] private CollisionHandler collisionHandler;

		private void OnEnable()
		{
			collisionHandler.CollisionStart += OnCollisionStart;
			collisionHandler.Collising += OnCollising;
		}

		private void OnDisable()
		{
			collisionHandler.CollisionStart -= OnCollisionStart;
			collisionHandler.Collising -= OnCollising;
		}

		private void OnCollisionStart(RaycastHit2D other)
		{
			if (other.transform == null) return;
			if (other.collider.isTrigger) return;


			Vector2 newVelocity = Vector2.Reflect(movable.Velocity, other.normal) / 2f;
			movable.Velocity = newVelocity;

			IMovable otherMovable = other.transform.gameObject.GetComponent<IMovable>();
			if (otherMovable == null) return;

			otherMovable.ApplyForce(movable.Velocity / 2f);
		}

		private void OnCollising(RaycastHit2D other)
		{
			transform.position += (transform.position - other.transform.position) * 0.01f;
		}
	}
}
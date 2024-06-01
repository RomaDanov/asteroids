using Contexts.Game.Components.Movement;
using UnityEngine;

public class Reflector : MonoBehaviour
{
	[SerializeField] private TransformMovement movable;
	[SerializeField] private CollisionHandler collisionHandler;

	private void OnEnable()
	{
		collisionHandler.CollisionStart += OnCollisionStart;
		collisionHandler.Collising += OnCollising;
		collisionHandler.CollisionEnd += OnCollisionEnd;
	}

	private void OnDisable()
	{
		collisionHandler.CollisionStart -= OnCollisionStart;
		collisionHandler.Collising -= OnCollising;
		collisionHandler.CollisionEnd -= OnCollisionEnd;
	}

	private void OnCollisionStart(RaycastHit2D other)
	{
		Vector2 newVelocity = Vector2.Reflect(movable.Velocity, other.normal) / 2f;

		IMovable otherMovable = other.transform.gameObject.GetComponent<IMovable>();
		if (otherMovable != null)
		{
			otherMovable.ApplyForce(movable.Velocity / 2f);
		}

		movable.Velocity = newVelocity;
	}

	private void OnCollising(RaycastHit2D other)
	{
		transform.position += (transform.position - other.transform.position) * 0.01f;
	}

	private void OnCollisionEnd(RaycastHit2D other)
	{

	}
}

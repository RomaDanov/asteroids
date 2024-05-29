using UnityEngine;

public class TransformMovement : MonoBehaviour, IMovable
{
	private Vector2 acceleration;

	public Vector2 Velocity { get; set; }

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
		acceleration *= 0;
	}

}

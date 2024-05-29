using UnityEngine;

public interface IMovable
{
	Vector2 Velocity { get; set; }
	void ApplyForce(Vector2 force);
}

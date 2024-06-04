using UnityEngine;

namespace Contexts.Game.Components.Movement
{
	public interface IMovable
	{
		Vector2 Acceleration { get; }
		Vector2 Velocity { get; set; }
		void ApplyForce(Vector2 force);
	}
}
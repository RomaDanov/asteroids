using UnityEngine;

namespace Contexts.Game.Components.Movement
{
	public interface IMovable
	{
		public enum AccelerationState
		{
			INCREASE,
			DECREASE
		}

		Vector2 Acceleration { get; }
		Vector2 Velocity { get; set; }
		AccelerationState State { get; }
		void ApplyForce(Vector2 force);
	}
}
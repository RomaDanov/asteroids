using Contexts.Game.Components.Movement;
using UnityEngine;

namespace Contexts.Game.Components.Asteroid
{
	[RequireComponent(typeof(IMovable))]
	public class AsteroidMovement : MonoBehaviour
	{
		[SerializeField] private TransformMovement movable;

		public void ForceStop()
		{
			movable.Velocity = Vector2.zero;
		}
	}
}
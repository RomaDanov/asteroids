using Contexts.Game.Components.Movements;
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
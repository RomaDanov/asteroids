using UnityEngine;

namespace Contexts.Game.Components.Collision
{
	public class CircleCollisionHandler : CollisionHandler
	{
		[SerializeField] private float size;

		public override RaycastHit2D[] CastAll()
		{
			return Physics2D.CircleCastAll(transform.position, size, Vector2.zero, 0, layerMask);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!enabled) return;

			Gizmos.color = color;
			Gizmos.DrawWireSphere(transform.position, size);
		}
#endif
	}
}
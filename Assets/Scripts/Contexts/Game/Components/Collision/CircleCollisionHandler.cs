using UnityEngine;

namespace Contexts.Game.Components.Collision
{
	public class CircleCollisionHandler : CollisionHandler
	{
		[SerializeField] private float size;

		public override RaycastHit2D[] CastAll()
		{
			RaycastHit2D[] hits = new RaycastHit2D[10];
			Physics2D.CircleCastNonAlloc(transform.position, size, Vector2.zero, hits, 0, layerMask);
			return hits;
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
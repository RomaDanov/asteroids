using UnityEngine;

namespace Contexts.Game.Components.Collision
{
	public class BoxCollisionHandler : CollisionHandler
	{
		[SerializeField] private Vector2 size;

		public override RaycastHit2D[] CastAll()
		{
			RaycastHit2D[] hits = new RaycastHit2D[10];
			Physics2D.BoxCastNonAlloc(transform.position, size, transform.rotation.z, Vector2.zero, hits, 0, layerMask);
			return hits;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!enabled) return;

			Gizmos.color = color;
			Gizmos.DrawWireCube(transform.position, size);
		}
#endif
	}
}
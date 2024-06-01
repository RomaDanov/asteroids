using UnityEngine;

namespace Contexts.Game.Components.Collision
{
	public class BoxCollisionHandler : CollisionHandler
	{
		[SerializeField] private Vector2 size;

		public override RaycastHit2D[] CastAll()
		{
			return Physics2D.BoxCastAll(transform.position, size, 0, Vector2.zero, layerMask);
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
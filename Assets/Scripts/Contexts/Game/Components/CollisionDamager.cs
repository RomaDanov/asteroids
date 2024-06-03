using Contexts.Game.Components.Collision;
using Extensions;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class CollisionDamager : MonoBehaviour
	{
		[SerializeField] private LayerMask targets;
		[SerializeField] private Health health;
		[SerializeField] private CollisionHandler collisionHandler;

		private float damage;

		public void Configure(float damage, LayerMask targets)
		{
			this.targets = targets;
			this.damage = damage;
		}

		private void OnEnable()
		{
			collisionHandler.CollisionStart += OnCollisionStart;
		}

		private void OnDisable()
		{
			collisionHandler.CollisionStart -= OnCollisionStart;
		}

		private void OnCollisionStart(RaycastHit2D other)
		{
			if (other.transform == null) return;

			int otherLayer = other.transform.gameObject.layer;
			if (!targets.Contains(otherLayer)) return;

			health.TakeDamage(damage);
		}
	}
}
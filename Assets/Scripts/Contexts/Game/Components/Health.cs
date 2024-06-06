using Architecture.ObjectPool;
using System;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Health : MonoBehaviour, IDamageable
	{
		public event Action<float> DamageTaken;
		public event Action Died;

		[SerializeField] private Explosion explosionFX;

		private float maxHealth;
		private float currentHealth;

		public bool IsAlive => currentHealth > 0;

		public void Configure(float maxHealth)
		{
			this.maxHealth = maxHealth;
			currentHealth = maxHealth;
		}

		public void TakeDamage(float damage)
		{
			if (!IsAlive)
			{
				Debug.LogError("Object already dead!");
				return;
			}

			if (damage <= 0)
			{
				Debug.LogError($"Ñan't deal {damage} damage!");
				return;
			}

			currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
			DamageTaken?.Invoke(damage);

			if (!IsAlive)
			{
				Die();
			}
		}

		public void Die()
		{
			Explode();
			Died?.Invoke();
		}

		private void Explode()
		{
			if (explosionFX == null) return;

			var explosionPool = ObjectPoolService.Instance.GetOrCreatePool(explosionFX, 10);
			Explosion explosion = explosionPool.Get();
			explosion.transform.position = transform.position;
		}
	}
}
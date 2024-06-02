using System;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Health : MonoBehaviour, IDamageable
	{
		public event Action<float> DamageTaken;
		public event Action Died;

		[SerializeField] private GameObject dyingFX;

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
			SpawnDyingFX();
			Died?.Invoke();
		}

		private void SpawnDyingFX()
		{
			//TODO:
		}
	}
}
using System;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class PlayerHealth : MonoBehaviour, IDamageable
	{
		public event Action<float> DamageTaken;
		public event Action Died;

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
				Debug.LogError("Player already dead!");
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
			Died?.Invoke();
			Destroy(gameObject);
		}
	}
}
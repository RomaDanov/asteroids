using System;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Health : MonoBehaviour, IDamageable
	{
		public event Action<float> DamageTaken;
		public event Action<Vector3> Died;

		public float MaxHealth { get; private set; }
		public float CurrentHealth { get; private set; }
		public bool IsAlive => CurrentHealth > 0;

		public void Configure(float maxHealth)
		{
			MaxHealth = maxHealth;
			CurrentHealth = maxHealth;
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

			CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
			DamageTaken?.Invoke(damage);

			if (!IsAlive)
			{
				Die();
			}
		}

		public void Die()
		{
			Died?.Invoke(transform.position);
		}
	}
}
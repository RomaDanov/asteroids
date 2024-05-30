using Configs.Weapons;
using ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Weapons.Projectile
{
	public class Projectile : PoolableObject<Projectile>
	{
		[SerializeField] private ProjectileDamageApplier damageApplier;
		[SerializeField] private ProjectileMovement movement;
		[SerializeField] private Destroyer destroyer;

		public virtual void Configure(ProjectileStats stats, DamageInfo damageInfo, Vector2 pushDirection)
		{
			movement?.Configure(stats, pushDirection);
			damageApplier?.Configure(damageInfo);
		}

		private void OnEnable()
		{
			damageApplier.Damaged += OnDamaged;
			destroyer.Destroyed += OnDestroyed;
		}

		private void OnDisable()
		{
			damageApplier.Damaged -= OnDamaged;
			destroyer.Destroyed -= OnDestroyed;
		}

		private void OnDamaged(List<IDamageable> targets)
		{
			Pool.Release(this);
		}

		private void OnDestroyed()
		{
			Pool.Release(this);
		}
	}
}
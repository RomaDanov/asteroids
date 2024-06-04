using Configs.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Weapons.Projectile
{
	public class MovableProjectile : Projectile
	{
		[Space]
		[Header("Movable")]
		[SerializeField] private ProjectileDamageApplier damageApplier;
		[SerializeField] private ProjectileMovement movement;

		public override void Configure(ProjectileStats stats, DamageInfo damageInfo)
		{
			movement.Configure(stats, transform.up);
			damageApplier.Configure(damageInfo);
			base.Configure(stats, damageInfo);

			damageApplier.DamageApplied += OnDamaged;
		}

		public override void Release()
		{
			base.Release();

			damageApplier.DamageApplied -= OnDamaged;
		}

		private void OnDamaged(List<IDamageable> targets)
		{
			if (!stats.DestroyOnCollision) return;

			Release();
		}
	}
}
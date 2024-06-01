using Configs.Weapons;
using Contexts.Game.Components.Zone;
using ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Weapons.Projectile
{
	public class Projectile : PoolableObject<Projectile>, IDeadzoneVisitor
	{
		[SerializeField] private ProjectileDamageApplier damageApplier;
		[SerializeField] private ProjectileMovement movement;
		[SerializeField] private CollisionHandler collisionHandler;

		public virtual void Configure(ProjectileStats stats, DamageInfo damageInfo, Vector2 pushDirection)
		{
			movement?.Configure(stats, pushDirection);
			damageApplier?.Configure(damageInfo);
			collisionHandler?.Configure(damageInfo.TargetLayers);
		}

		private void OnEnable()
		{
			damageApplier.DamageApplied += OnDamaged;
		}

		private void OnDisable()
		{
			damageApplier.DamageApplied -= OnDamaged;
		}

		private void Release()
		{
			movement.ForceStop();
			Pool.Release(this);
		}

		private void OnDamaged(List<IDamageable> targets)
		{
			Release();
		}

		public void Visit()
		{
			Release();
		}
	}
}
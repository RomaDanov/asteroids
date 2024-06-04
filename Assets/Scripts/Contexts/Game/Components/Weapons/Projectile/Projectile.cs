using Configs.Weapons;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
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

			collisionHandler.enabled = true;
			damageApplier.DamageApplied += OnDamaged;

			gameObject.SetActive(true);
		}

		private void Release()
		{
			damageApplier.DamageApplied -= OnDamaged;

			collisionHandler.enabled = false;
			movement.ForceStop();
			gameObject.SetActive(false);
			Pool.Release(this);
		}

		public void Visit()
		{
			Release();
		}

		private void OnDamaged(List<IDamageable> targets)
		{
			Release();
		}
	}
}
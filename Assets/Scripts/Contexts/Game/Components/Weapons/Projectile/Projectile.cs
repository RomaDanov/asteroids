using Configs.Weapons;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
using ObjectPool;
using UnityEngine;

namespace Contexts.Game.Components.Weapons.Projectile
{
	public class Projectile : PoolableObject<Projectile>, IDeadzoneVisitor
	{
		[Header("Base")]
		[SerializeField] protected CollisionHandler collisionHandler;

		protected ProjectileStats stats;

		public virtual void Configure(ProjectileStats stats, DamageInfo damageInfo)
		{
			this.stats = stats;
			collisionHandler.Configure(damageInfo.TargetLayers);
			collisionHandler.enabled = true;
			gameObject.SetActive(true);
		}

		public virtual void Release()
		{
			collisionHandler.enabled = false;
			gameObject.SetActive(false);
			Pool.Release(this);
		}

		public void Visit()
		{
			Release();
		}
	}
}
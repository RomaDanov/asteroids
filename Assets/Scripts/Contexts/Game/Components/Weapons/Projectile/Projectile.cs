using Architecture.ObjectPool;
using Configs.Weapons;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
using UnityEngine;

namespace Contexts.Game.Components.Weapons.Projectile
{
	public class Projectile : PoolableObject<Projectile>, IDeadzoneVisitor
	{
		[Header("Base")]
		[SerializeField] protected CollisionHandler collisionHandler;

		protected ProjectileStats stats;

		public override void Init(IObjectPool<Projectile> pool)
		{
			base.Init(pool);
			collisionHandler.enabled = false;
		}

		public virtual void Configure(ProjectileStats stats, DamageInfo damageInfo)
		{
			this.stats = stats;
			collisionHandler.Configure(damageInfo.TargetLayers);
			collisionHandler.enabled = true;

			gameObject.SetActive(true);
		}

		public virtual void Release()
		{
			gameObject.SetActive(false);

			collisionHandler.enabled = false;
			Pool.Release(this);
		}

		public void Visit()
		{
			Release();
		}
	}
}
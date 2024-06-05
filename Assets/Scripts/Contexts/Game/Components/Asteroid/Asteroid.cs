using Architecture.ObjectPool;
using Configs.Asteriods;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
using UnityEngine;

namespace Contexts.Game.Components.Asteroid
{
	public class Asteroid : PoolableObject<Asteroid>, IDeadzoneVisitor
	{
		[SerializeField] private LayerMask targets;
		[Space]
		[Header("Components")]
		[SerializeField] private Rotator rotator;
		[SerializeField] private AsteroidFragmentsSpawner fragmentSpawner;
		[SerializeField] private Health health;
		[SerializeField] private AsteroidMovement movement;
		[SerializeField] private CollisionDamager collisionDamager;
		[SerializeField] private CollisionHandler collisionHandler;

		public override void Init(IObjectPool<Asteroid> pool)
		{
			base.Init(pool);
			collisionHandler.enabled = false;
		}

		public void Configure(AsteroidConfig config)
		{
			rotator.Configure(Random.Range(-config.RotateSpeed, config.RotateSpeed));
			fragmentSpawner.Configure(config.DestructionFragments, config.FragmentsCount);
			health.Configure(config.MaxHealth);
			collisionDamager.Configure(1, targets);
			collisionHandler.enabled = true;

			health.Died += OnDied;

			gameObject.SetActive(true);
		}

		private void Release()
		{
			gameObject.SetActive(false);

			health.Died -= OnDied;

			collisionHandler.enabled = false;
			movement.ForceStop();
			fragmentSpawner.TrySpawnFragments();

			Pool.Release(this);
		}

		public void Visit()
		{
			Release();
		}

		private void OnDied()
		{
			Release();
		}
	}
}
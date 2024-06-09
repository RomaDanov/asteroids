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
		[SerializeField] private HealthView healthView;
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
			health.Died += OnDied;

			rotator.Configure(Random.Range(-config.RotateSpeed, config.RotateSpeed));
			fragmentSpawner.Configure(config.DestructionFragments, config.FragmentsCount);
			collisionDamager.Configure(1, targets);

			health.Configure(config.MaxHealth);
			healthView.Configure();

			collisionHandler.enabled = true;
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

		private void OnDied(Vector3 position)
		{
			Release();
		}
	}
}
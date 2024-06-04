using Configs.Asteriods;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
using ObjectPool;
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

		public void Configure(AsteroidConfig config)
		{
			rotator.Configure(Random.Range(-config.RotateSpeed, config.RotateSpeed));
			fragmentSpawner.Configure(config.DestructionFragments, config.FragmentsCount);
			health.Configure(config.MaxHealth);
			collisionDamager.Configure(1, targets);
		}

		public override void OnGet()
		{
			collisionHandler.enabled = true;
			health.Died += OnDied;
		}

		public override void OnRelease()
		{
			collisionHandler.enabled = false;
			health.Died -= OnDied;
		}

		private void Release()
		{
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
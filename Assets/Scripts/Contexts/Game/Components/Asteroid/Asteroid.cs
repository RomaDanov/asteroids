using Configs.Asteriods;
using Contexts.Game.Components.Player;
using Contexts.Game.Components.Zone;
using Messages;
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

		private AsteroidConfig config;

		public void Configure(AsteroidConfig config)
		{
			this.config = config;

			rotator.Configure(Random.Range(-config.RotateSpeed, config.RotateSpeed));
			fragmentSpawner.Configure(config.DestructionFragments);
			health.Configure(config.MaxHealth);
		}

		private void OnEnable()
		{
			health.Died += OnDied;
		}

		private void OnDisable()
		{
			health.Died -= OnDied;
		}

		private void Release()
		{
			movement.ForceStop();
			fragmentSpawner.TrySpawnFragments();
			Pool.Release(this);
			MessageRouter.Instance.Publish(new GameMessages.AsteroidDestroyedMessage(config.Id));
		}

		private void OnDied()
		{
			Release();
		}

		public void Visit()
		{
			Release();
		}
	}
}
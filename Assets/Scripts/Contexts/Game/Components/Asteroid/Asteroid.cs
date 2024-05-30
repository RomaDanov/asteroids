using Configs.Asteriods;
using Contexts.Game.Components.Movement;
using Contexts.Game.Components.Player;
using ObjectPool;
using UnityEngine;

namespace Contexts.Game.Components.Asteroid
{
	public class Asteroid : PoolableObject<Asteroid>
	{
		[SerializeField] private LayerMask targets;
		[Space]
		[Header("Components")]
		[SerializeField] private Rotator rotator;
		[SerializeField] private Health health;

		private AsteroidConfig config;

		public void Configure(AsteroidConfig config)
		{
			this.config = config;
			rotator.Configure(Random.Range(-config.RotateSpeed, config.RotateSpeed));
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

		private void OnDied() 
		{
			Pool.Release(this);
		}
	}
}
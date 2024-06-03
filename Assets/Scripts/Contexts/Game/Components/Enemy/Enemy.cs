using Configs.Enemies;
using Configs.Weapons;
using Contexts.Game.Components.Fence;
using ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Enemy
{
	public class Enemy : PoolableObject<Enemy>, IDeadzoneVisitor
	{
		[SerializeField] private LayerMask targets;
		[Space]
		[Header("Components")]
		[SerializeField] private EnemyMovement movement;
		[SerializeField] private EnemyAttack attack;
		[SerializeField] private ShipInstaller shipInstaller;
		[SerializeField] private Health health;
		[SerializeField] private Equipments equipments;
		[SerializeField] private CollisionDamager collisionDamager;

		public void Configure(EnemyConfig config)
		{
			shipInstaller.Install(config.ShipConfig);
			movement.Configure(config.ShipConfig.MovementSettings, config.StoppingDistance);
			health.Configure(config.ShipConfig.MaxHealth);

			if (config.WeaponConfig != null)
			{
				equipments.Configure(new List<WeaponConfig> { config.WeaponConfig }, targets);
			}

			attack.Configure(config.AttackRange);
			collisionDamager.Configure(1, targets);
		}

		private void OnEnable()
		{
			health.Died += OnDied;
		}

		private void OnDisable()
		{
			health.Died -= OnDied;
		}

		public void Visit()
		{
			Release();
		}

		private void Release()
		{
			shipInstaller.Uninstall();
			movement.ForceStop();
			Pool.Release(this);
		}

		private void OnDied()
		{
			Release();
		}
	}
}
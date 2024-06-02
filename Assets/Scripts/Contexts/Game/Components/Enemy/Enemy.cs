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

		public void Configure(EnemyConfig config)
		{
			shipInstaller.Install(config.ShipConfig);
			movement.Configure(config.ShipConfig.MovementSettings, config.StoppingDistance);
			health.Configure(config.ShipConfig.MaxHealth);
			equipments.Configure(new List<WeaponConfig> { config.WeaponConfig }, targets);
			attack.Configure(config.AttackRange);
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
			Pool.Release(this);
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
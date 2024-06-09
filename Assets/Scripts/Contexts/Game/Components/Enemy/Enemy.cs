using Architecture.ObjectPool;
using Configs.Enemies;
using Configs.Weapons;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
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
		[SerializeField] private HealthView healthView;
		[SerializeField] private Equipments equipments;
		[SerializeField] private CollisionDamager collisionDamager;
		[SerializeField] private CollisionHandler collisionHandler;

		public override void Init(IObjectPool<Enemy> pool)
		{
			base.Init(pool);
			collisionHandler.enabled = false;
		}

		public void Configure(EnemyConfig config)
		{
			health.Died += OnDied;

			shipInstaller.Install(config.ShipConfig);
			movement.ApplySettings(config.ShipConfig.MovementSettings);
			movement.Configure(config.StoppingDistance);

			health.Configure(config.ShipConfig.MaxHealth);
			healthView.Configure();

			if (config.WeaponConfig != null)
			{
				equipments.Configure(new List<WeaponConfig> { config.WeaponConfig }, targets);
			}

			attack.Configure(config.AttackRange);
			collisionDamager.Configure(1, targets);

			collisionHandler.enabled = true;

			movement.MoveProccessing += shipInstaller.Ship.SetActiveEngineFX;

			gameObject.SetActive(true);
		}

		private void Release()
		{
			gameObject.SetActive(false);

			health.Died -= OnDied;
			movement.MoveProccessing -= shipInstaller.Ship.SetActiveEngineFX;

			collisionHandler.enabled = false;
			shipInstaller.Uninstall();
			movement.ForceStop();
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
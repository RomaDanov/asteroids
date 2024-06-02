using Configs.Enemies;
using Configs.Weapons;
using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Fence;
using ObjectPool;
using System.Collections.Generic;
using Unity.VisualScripting;
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
		[SerializeField] private CollisionHandler collisionHandler;

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
		}

		private void OnEnable()
		{
			health.Died += OnDied;
			collisionHandler.CollisionStart += OnCollisionStart;
		}

		private void OnDisable()
		{
			health.Died -= OnDied;
			collisionHandler.CollisionStart -= OnCollisionStart;
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

		private void OnCollisionStart(RaycastHit2D other)
		{
			if (other.transform == null) return;

			IDamageable damageable = other.transform.GetComponent<IDamageable>();
			if (damageable != null) damageable.TakeDamage(1);

			health.Die();
		}
	}
}
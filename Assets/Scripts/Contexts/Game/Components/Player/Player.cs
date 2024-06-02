using Configs.Ships;
using Configs.Weapons;
using Contexts.Game.Components.Collision;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private LayerMask targets;
		[Space]
		[Header("Components")]
		[SerializeField] private ShipInstaller shipInstaller;
		[SerializeField] private PlayerMovement movement;
		[SerializeField] private Health health;
		[SerializeField] private Equipments equipments;
		[SerializeField] private CollisionHandler collisionHandler;

		public void Configure(ShipConfig shipConfig, IReadOnlyCollection<WeaponConfig> weapons)
		{
			shipInstaller.Install(shipConfig);
			movement?.Configure(shipConfig.MovementSettings);
			health?.Configure(shipConfig.MaxHealth);
			equipments?.Configure(weapons, targets);
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

		private void OnDied()
		{
			Destroy(gameObject);
		}

		private void OnCollisionStart(RaycastHit2D other)
		{
			health.Die();
		}
	}
}
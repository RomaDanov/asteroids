using Configs.Ships;
using Configs.Weapons;
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
		[SerializeField] private CollisionDamager collisionDamager;

		public void Configure(ShipConfig shipConfig, IReadOnlyCollection<WeaponConfig> weapons)
		{
			shipInstaller.Install(shipConfig);
			movement.Configure(shipConfig.MovementSettings);
			health.Configure(shipConfig.MaxHealth);
			equipments.Configure(weapons, targets);
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

		private void OnDied()
		{
			shipInstaller.Uninstall();
			Destroy(gameObject);
		}
	}
}
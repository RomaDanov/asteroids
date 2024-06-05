using Architecture.Messages;
using Configs.Ships;
using Configs.Weapons;
using Contexts.Game.Components.Movements;
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
		[SerializeField] private Movement movement;
		[SerializeField] private Health health;
		[SerializeField] private Equipments equipments;
		[SerializeField] private CollisionDamager collisionDamager;

		public void Configure(ShipConfig shipConfig, IReadOnlyCollection<WeaponConfig> weapons)
		{
			shipInstaller.Install(shipConfig);
			movement.ApplySettings(shipConfig.MovementSettings);
			health.Configure(shipConfig.MaxHealth);
			equipments.Configure(weapons, targets);
			collisionDamager.Configure(1, targets);

			health.Died += OnDied;
			movement.MoveProccessing += shipInstaller.Ship.SetActiveEngineFX;
		}

		private void OnDied()
		{
			health.Died -= OnDied;
			movement.MoveProccessing -= shipInstaller.Ship.SetActiveEngineFX;

			shipInstaller.Uninstall();
			Destroy(gameObject);

			MessageRouter.Instance.Publish<GameMessages.PlayerDiedMessage>();
		}
	}
}
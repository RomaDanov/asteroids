using Configs.Ships;
using Configs.Weapons;
using Contexts.Game.Factories;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Transform shipRoot;
		[SerializeField] private LayerMask targets;
		[Space]
		[Header("Components")]
		[SerializeField] private PlayerMovement movement;
		[SerializeField] private Health health;
		[SerializeField] private PlayerEquipments equipments;
		[SerializeField] private CollisionHandler collisionHandler;

		public void Configure(ShipConfig shipConfig, IReadOnlyCollection<WeaponConfig> weapons)
		{
			ConfigureShip(shipConfig);
			movement?.Configure(shipConfig.MovementSettings);
			health?.Configure(shipConfig.MaxHealth);
			equipments?.Configure(weapons, targets);
		}

		private void ConfigureShip(ShipConfig ship)
		{
			ShipCreator creator = new ShipCreator();
			creator.Create(ship, shipRoot);
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
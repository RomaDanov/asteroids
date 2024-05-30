using Configs.Ships;
using Configs.Weapons;
using Contexts.Game.Factories;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Transform shipRoot;
		[SerializeField] private PlayerMovement movement;
		[SerializeField] private PlayerHealth health;
		[SerializeField] private PlayerEquipments equipments;
		[SerializeField] private PlayerAttack attack;

		public void Configure(ShipConfig shipConfig, IReadOnlyCollection<WeaponConfig> weapons)
		{
			ConfigureShip(shipConfig);
			movement?.Configure(shipConfig.MovementSettings);
			health?.Configure(shipConfig.MaxHealth);
			equipments?.Configure(weapons);
		}

		private void ConfigureShip(ShipConfig ship)
		{
			ShipCreator creator = new ShipCreator();
			creator.Create(ship, shipRoot);
		}
	}
}
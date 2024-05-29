using Configs.Ships;
using Configs.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
		creator.Create(ship, transform);
	}
}

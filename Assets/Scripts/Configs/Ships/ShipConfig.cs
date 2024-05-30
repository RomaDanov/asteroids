using System;
using UnityEngine;

namespace Configs.Ships
{
	[CreateAssetMenu(fileName = "ShipConfig", menuName = "Configs/Ships/Config")]
	public class ShipConfig : Config
	{
		[Space]
		[Header("Ship")]
		[SerializeField, Min(0)] private int maxHealth;
		[SerializeField] private ShipView prefab;
		[SerializeField] private MovementSettings movementSettings;

		public int MaxHealth => maxHealth;
		public ShipView Prefab => prefab;
		public MovementSettings MovementSettings => movementSettings;
	}
}
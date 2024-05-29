using System;
using UnityEngine;

namespace Configs.Ships
{
	[CreateAssetMenu(fileName = "ShipConfig", menuName = "Configs/Ships/Config")]
	public class ShipConfig : Config
	{
		[Header("Ship")]
		[Space]
		[SerializeField, Min(0)] private int maxHealth;
		[SerializeField] private ShipView prefab;
		[SerializeField] private MovementSettings movementSettings;

		public int MaxHealth => maxHealth;
		public ShipView Prefab => prefab;
		public MovementSettings MovementSettings => movementSettings;
	}

	[Serializable]
	public class MovementSettings
	{
		[SerializeField, Min(0)] private float maxSpeed;
		[SerializeField, Min(0)] private float acceleration;
		[SerializeField, Min(0)] private float torq;
		[SerializeField, Min(0)] private float brakeForce;

		public float MaxSpeed => maxSpeed;
		public float Acceleration => acceleration;
		public float Torq => torq;
		public float BrakeForce => brakeForce;
	}
}
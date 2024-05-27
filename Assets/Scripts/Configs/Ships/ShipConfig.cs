using UnityEngine;

namespace Configs.Ships
{
	[CreateAssetMenu(fileName = "ShipConfig", menuName = "Configs/Ships/Config")]
	public class ShipConfig : Config
	{
		[Header("Ship")]
		[Space]
		[SerializeField, Min(0)] private int maxHealth;
		[SerializeField, Min(0)] private float maxSpeed;
		[SerializeField, Min(0)] private float acceleration;
		[SerializeField, Min(0)] private float torq;
		[SerializeField, Min(0)] private float brakingSpeed;
		[SerializeField] private ShipController prefab;

		public int MaxHealth => maxHealth;
		public float MaxSpeed => maxSpeed;
		public float Acceleration => acceleration;
		public float Torq => torq;
		public float BrakingSpeed => brakingSpeed;
		public ShipController Prefab => prefab;
	}
}
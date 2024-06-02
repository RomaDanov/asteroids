using Configs.Ships;
using Configs.Weapons;
using Contexts.Game.Components.Enemy;
using UnityEngine;

namespace Configs.Enemies
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemies/Config")]
	public class EnemyConfig : Config
	{
		[Space]
		[Header("Enemy")]
		[SerializeField] private float stoppingDistance;
		[SerializeField] private float attackRange;
		[SerializeField] private ShipConfig shipConfig;
		[SerializeField] private WeaponConfig weaponConfig;
		[SerializeField] private Enemy prefab;

		public float StoppingDistance => stoppingDistance;
		public float AttackRange => attackRange;
		public ShipConfig ShipConfig => shipConfig;
		public WeaponConfig WeaponConfig => weaponConfig;
		public Enemy Prefab => prefab;
	}
}
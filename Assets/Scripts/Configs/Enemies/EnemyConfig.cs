using Configs.Ships;
using Configs.Weapons;
using UnityEngine;

namespace Configs.Enemies
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemies/Config")]
	public class EnemyConfig : Config
	{
		[Space]
		[Header("Enemy")]
		[SerializeField] private ShipConfig shipConfig;
		[SerializeField] private WeaponConfig weaponConfig;
		[SerializeField] private GameObject prefab;

		public GameObject Prefab => prefab;
	}
}
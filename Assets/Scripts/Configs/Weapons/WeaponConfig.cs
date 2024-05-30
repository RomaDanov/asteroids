using UnityEngine;

namespace Configs.Weapons
{
	[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapons/Config")]
	public class WeaponConfig : Config
	{
		[Space]
		[Header("Weapon")]
		[SerializeField] private WeaponStats weaponStats;
		[SerializeField] private Weapon prefab;

		public WeaponStats WeaponStats => weaponStats;
		public Weapon Prefab => prefab;
	}
}
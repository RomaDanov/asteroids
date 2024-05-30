using Contexts.Game.Components.Weapons.Projectile;
using UnityEngine;

namespace Configs.Weapons
{
	[CreateAssetMenu(fileName = "ProjectileWeaponConfig", menuName = "Configs/Weapons/ProjectileWeaponConfig")]
	public class ProjectileWeaponConfig : WeaponConfig
	{
		[Space]
		[Header("Projectile Weapon")]
		[SerializeField] private ProjectileStats projectileStats;
		[SerializeField] private Projectile projectilePrefab;

		public ProjectileStats ProjectileStats => projectileStats;
		public Projectile ProjectilePrefab => projectilePrefab;
	}
}
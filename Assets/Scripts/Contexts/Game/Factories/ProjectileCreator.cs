using Configs.Weapons;
using Contexts.Game.Components.Weapons;
using Contexts.Game.Components.Weapons.Projectile;
using DataProviders;
using ObjectPool;
using ServiceLocator;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public class ProjectileCreator : IProjectileCreator
	{
		public Projectile Create(string weaponId, DamageInfo damageInfo, Vector3 startedPosition, Vector2 pushDirection)
		{
			WeaponsDataProvider weaponsDataProvider = ServicesManager.Instance.Get<WeaponsDataProvider>();
			WeaponConfig config = weaponsDataProvider.GetWeaponConfig(weaponId);

			if (config == null)
			{
				Debug.LogError($"Weapon with id {weaponId} doesn't exist");
				return null;
			}
			else if (config is not ProjectileWeaponConfig)
			{
				Debug.LogError($"Weapon {weaponId} is not projectilable!");
				return null;
			}

			return Create(config as ProjectileWeaponConfig, damageInfo, startedPosition, pushDirection);
		}

		public Projectile Create(ProjectileWeaponConfig config, DamageInfo damageInfo, Vector3 startedPosition, Vector2 pushDirection)
		{
			if (config == null)
			{
				Debug.LogError($"Ship with id {config} doesn't exist");
				return null;
			}

			Projectile prefabRef = config.ProjectilePrefab;

			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefabRef, 20);

			Projectile projectile = pool.Get();
			projectile.transform.position = startedPosition;
			projectile.Configure(config.ProjectileStats, damageInfo, pushDirection);

			return projectile;
		}
	}
}
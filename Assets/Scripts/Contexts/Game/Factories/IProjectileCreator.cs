using Configs.Weapons;
using Contexts.Game.Components.Weapons;
using Contexts.Game.Components.Weapons.Projectile;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public interface IProjectileCreator
	{
		Projectile Create(string weapnoId, DamageInfo damageInfo, Vector3 startedPosition, Vector2 pushDirection);
		Projectile Create(ProjectileWeaponConfig config, DamageInfo damageInfo, Vector3 startedPosition, Vector2 pushDirection);
	}
}
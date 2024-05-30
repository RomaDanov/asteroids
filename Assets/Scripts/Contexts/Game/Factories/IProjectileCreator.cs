using Configs.Weapons;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public interface IProjectileCreator
	{
		Projectile Create(string weapnoId, Vector3 startedPosition, Vector2 pushDirection);
		Projectile Create(ProjectileWeaponConfig config, Vector3 startedPosition, Vector2 pushDirection);
	}
}
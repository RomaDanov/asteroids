using Configs.Weapons;
using UnityEngine;

namespace Contexts.Game.Components.Weapons
{
	public class WeaponProjectile : Weapon
	{
		private WeaponConfig config;

		private int currentAmmoCount;
		private float currentReloadTime;
		private float currentShotIntervalTime;

		public bool IsReloading => currentReloadTime > 0;

		public override void Configure(WeaponConfig config)
		{
			this.config = config;
		}

		private void Update()
		{
			if (currentAmmoCount <= 0)
			{
				if (!IsReloading)
				{
					StartReloading();
				}
			}

			if (currentReloadTime > 0)
			{
				currentReloadTime -= Time.deltaTime;
				if (currentReloadTime <= 0)
				{
					StopReloading();
					RestoreClip();
				}
			}

			if (currentShotIntervalTime > 0)
			{
				currentShotIntervalTime -= Time.deltaTime;
				if (currentShotIntervalTime <= 0)
				{
					StopAttackInterval();
				}
			}
		}

		public override bool TryAttack()
		{
			if (IsReloading) return false;
			if (currentShotIntervalTime > 0) return false;

			for (int i = 0; i < config.ProjectileCount; i++)
			{
				Debug.Log("SHOOT!");
			}

			currentAmmoCount--;
			StartAttackInterval();

			return true;
		}

		private void StartReloading()
		{
			currentReloadTime = config.ReloadTime;
		}

		private void StopReloading()
		{
			currentReloadTime = 0;
		}

		private void RestoreClip()
		{
			currentAmmoCount = config.ClipCapacity;
		}

		private void StartAttackInterval()
		{
			currentShotIntervalTime = config.ShotInterval;
		}

		private void StopAttackInterval()
		{
			currentShotIntervalTime = 0;
		}
	}
}
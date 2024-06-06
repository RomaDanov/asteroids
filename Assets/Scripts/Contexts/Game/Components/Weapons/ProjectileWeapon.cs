using Configs.Weapons;
using Contexts.Game.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Weapons
{
	public class ProjectileWeapon : Weapon
	{
		[SerializeField] private Transform[] projectilePivots;

		private ProjectileWeaponConfig config;
		private LayerMask targetLayers;

		private ProjectileCreator projectileCreator;
		private int currentAmmoCount;
		private float currentReloadTime;
		private float currentShotIntervalTime;
		private Coroutine attackProccesingCoroutine;

		public bool IsReloading => currentReloadTime > 0;

		public override void Configure(WeaponConfig config, LayerMask targetLayers)
		{
			projectileCreator = new ProjectileCreator();
			this.config = config as ProjectileWeaponConfig;
			this.targetLayers = targetLayers;
			RestoreClip();
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
			if (attackProccesingCoroutine != null) return false;

			attackProccesingCoroutine = StartCoroutine(SpawnProjectilesCoroutine());

			currentAmmoCount--;
			StartAttackInterval();

			return true;
		}

		private IEnumerator SpawnProjectilesCoroutine()
		{
			yield return null;

			List<(Vector3 position, Quaternion rotation)> pivotTransforms = new();

			for (int i = 0; i < config.ProjectileStats.Count; i++)
			{
				Transform pivot = GetPivot(i);
				pivotTransforms.Add((pivot.position, pivot.rotation));
			}

			for (int i = 0; i < config.ProjectileStats.Count; i++)
			{
				DamageInfo damageInfo = new DamageInfo(config.WeaponStats.Damage, config.WeaponStats.Range, targetLayers);
				projectileCreator.Create(config, damageInfo, pivotTransforms[i].position, pivotTransforms[i].rotation);
				 if(config.ProjectileStats.Interval > 0) yield return new WaitForSeconds(config.ProjectileStats.Interval);
			}

			attackProccesingCoroutine = null;
		}

		private Transform GetPivot(int i)
		{
			return i >= 0 && i < projectilePivots.Length ? projectilePivots[i] : projectilePivots[0];
		}

		private void StartReloading()
		{
			currentReloadTime = config.WeaponStats.ReloadTime;
		}

		private void StopReloading()
		{
			currentReloadTime = 0;
		}

		private void RestoreClip()
		{
			currentAmmoCount = config.WeaponStats.ClipCapacity;
		}

		private void StartAttackInterval()
		{
			currentShotIntervalTime = config.WeaponStats.ShotInterval;
		}

		private void StopAttackInterval()
		{
			currentShotIntervalTime = 0;
		}
	}
}
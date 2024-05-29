using Contexts.Game.Components.Weapons;
using System;
using UnityEngine;

namespace Configs.Weapons
{
	[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapons/Config")]
	public class WeaponConfig : Config
	{
		[Header("Weapon")]
		[Space]
		[SerializeField] private int damage;
		[SerializeField] private int clipCapacity;
		[SerializeField] private float reloadTime;
		[SerializeField] private float shotInterval;
		[SerializeField] private int projectileCount;
		[SerializeField] private int projectileInterval;
		[SerializeField] private float projectileSpeed;
		[SerializeField] private GameObject projectilePrefab;
		[SerializeField] private Weapon prefab;

		public int Damage => damage;
		public int ClipCapacity => clipCapacity;
		public float ReloadTime => reloadTime;
		public float ShotInterval => shotInterval;
		public int ProjectileCount => projectileCount;
		public int ProjectileInterval => projectileInterval;
		public float ProjectileSpeed => projectileSpeed;
		public GameObject ProjectilePrefab => projectilePrefab;
		public Weapon Prefab => prefab;
	}
}
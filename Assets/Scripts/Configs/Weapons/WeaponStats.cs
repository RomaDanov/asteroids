using System;
using UnityEngine;

namespace Configs.Weapons
{
	[Serializable]
	public struct WeaponStats
	{
		[SerializeField] private int damage;
		[SerializeField] private int clipCapacity;
		[SerializeField] private float reloadTime;
		[SerializeField] private float attackInterval;
		[SerializeField] private float range;

		public int Damage => damage;
		public int ClipCapacity => clipCapacity;
		public float ReloadTime => reloadTime;
		public float ShotInterval => attackInterval;
		public float Range => range;
	}
}
using System;
using UnityEngine;

namespace Configs.Weapons
{
	[Serializable]
	public struct ProjectileStats
	{
		[SerializeField] private int count;
		[SerializeField] private float interval;
		[SerializeField] private float speed;

		public int Count => count;
		public float Interval => interval;
		public float Speed => speed;
	}
}
using UnityEngine;

namespace Contexts.Game.Components.Weapons
{
	public struct DamageInfo
	{
		public float Damage { get; private set; }
		public float Range { get; private set; }
		public LayerMask TargetLayers { get; private set; }

		public DamageInfo(float damage, float range, LayerMask targetLayers)
		{
			Damage = damage;
			Range = range;
			TargetLayers = targetLayers;
		}
	}
}
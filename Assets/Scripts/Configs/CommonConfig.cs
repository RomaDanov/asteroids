using Configs.Ships;
using Configs.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "CommonConfig", menuName = "Configs/Common/Config")]
	public class CommonConfig : ScriptableObject
	{
		[SerializeField] private ShipConfig defaultPlayerShip;
		[SerializeField] private WeaponConfig[] defaultPlayerWeapons;

		public ShipConfig DefaultPlayerShip => defaultPlayerShip;
		public IReadOnlyCollection<WeaponConfig> DefaultPlayerWeapons => defaultPlayerWeapons;
	}
}
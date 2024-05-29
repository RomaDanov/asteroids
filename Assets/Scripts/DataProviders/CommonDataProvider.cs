using Configs;
using Configs.Ships;
using Configs.Weapons;
using ServiceLocator;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace DataProviders
{
	public class CommonDataProvider : ServiceInstance
	{
		protected CommonConfig config;

		public override void PreLoad()
		{
			config = AssetDatabase.LoadAssetAtPath<CommonConfig>(Path.Combine("Assets", "Resources", "Configs", $"{nameof(CommonConfig)}.asset"));
		}

		public ShipConfig GetDefaultPlayerShip() => config.DefaultPlayerShip;
		public IReadOnlyCollection<WeaponConfig> GetDefaultPlayerWeapons() => config.DefaultPlayerWeapons;
	}
}
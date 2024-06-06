using Architecture.ServiceLocator;
using Configs;
using Configs.Ships;
using Configs.Weapons;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Architecture.Inputs.GamepadInput;
using static Configs.CommonConfig;

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

		public Sprite GetGamepadButtonIcon(GamepadButtonType button)
		{
			GamepadIconLink link = config.GamepadIconLinks.FirstOrDefault(x => x.Button == button);
			if (link == null) return null;

			return link.Icon;
		}
	}
}
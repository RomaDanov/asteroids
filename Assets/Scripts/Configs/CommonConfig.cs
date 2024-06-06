using Configs.Ships;
using Configs.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Architecture.Inputs.GamepadInput;

namespace Configs
{
	[CreateAssetMenu(fileName = "CommonConfig", menuName = "Configs/Common/Config")]
	public class CommonConfig : ScriptableObject
	{
		[SerializeField] private ShipConfig defaultPlayerShip;
		[SerializeField] private WeaponConfig[] defaultPlayerWeapons;
		[Space]
		[SerializeField] private GamepadIconLink[] gamepadIconLinks;

		public ShipConfig DefaultPlayerShip => defaultPlayerShip;
		public IReadOnlyCollection<WeaponConfig> DefaultPlayerWeapons => defaultPlayerWeapons;
		public IReadOnlyCollection<GamepadIconLink> GamepadIconLinks => gamepadIconLinks;

		[Serializable]
		public class GamepadIconLink
		{
			[SerializeField] private GamepadButtonType button;
			[SerializeField] private Sprite icon;

			public GamepadButtonType Button => button;
			public Sprite Icon => icon;
		}
	}
}
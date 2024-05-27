using System.Collections.Generic;
using UnityEngine;

namespace Configs.Weapons
{
	[CreateAssetMenu(fileName = "WeaponsLibrary", menuName = "Configs/Weapons/Library")]
	public class WeaponsLibraryConfig : LibraryConfig<WeaponConfig>
	{
		public override IReadOnlyCollection<WeaponConfig> Items => base.Items;
	}
}
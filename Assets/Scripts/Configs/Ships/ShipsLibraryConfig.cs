using System.Collections.Generic;
using UnityEngine;

namespace Configs.Ships
{
	[CreateAssetMenu(fileName = "ShipsLibrary", menuName = "Configs/Ships/Library")]
	public class ShipsLibraryConfig : LibraryConfig<ShipConfig>
	{
		public override IReadOnlyCollection<ShipConfig> Items => base.Items;
	}
}
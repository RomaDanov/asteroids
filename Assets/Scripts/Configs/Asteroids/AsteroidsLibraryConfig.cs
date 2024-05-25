using System.Collections.Generic;
using UnityEngine;

namespace Configs.Asteriods
{
	[CreateAssetMenu(fileName = "AsteroidsLibrary", menuName = "Configs/Asteroids/Library")]
	public class AsteroidsLibraryConfig : LibraryConfig<AsteroidConfig>
	{
		public override IReadOnlyCollection<AsteroidConfig> Items => base.Items;
	}
}
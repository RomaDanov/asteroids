using System.Collections.Generic;
using UnityEngine;

namespace Configs.Enemies
{
	[CreateAssetMenu(fileName = "EnemiesLibrary", menuName = "Configs/Enemies/Library")]
	public class EnemiesLibraryConfig : LibraryConfig<EnemyConfig>
	{
		public override IReadOnlyCollection<EnemyConfig> Items => base.Items;
	}
}
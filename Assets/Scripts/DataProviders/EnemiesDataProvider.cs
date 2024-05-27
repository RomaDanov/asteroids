using Configs.Enemies;
using DataProviders;
using System.Linq;

namespace DataProviders
{
	public class EnemiesDataProvider : DataProvider<EnemiesLibraryConfig>
	{
		public EnemyConfig GetEnemyConfig(string id)
		{
			EnemyConfig config = library.Items.FirstOrDefault(x => x.Id == id);
			return config;
		}
	}
}
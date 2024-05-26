using Configs.Enemies;
using System.Linq;

public class EnemiesDataProvider : DataProvider<EnemiesLibraryConfig>
{
	public EnemyConfig GetEnemyConfig(string id)
	{
		EnemyConfig config = library.Items.FirstOrDefault(x => x.Id == id);
		return config;
	}
}

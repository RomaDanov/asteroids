using Configs.Asteriods;
using System.Linq;

public class AsteroidsDataProvider : DataProvider<AsteroidsLibraryConfig>
{
	public AsteroidConfig GetAsteroidConfig(string id)
	{
		AsteroidConfig config = library.Items.FirstOrDefault(x => x.Id == id);
		return config;
	}
}

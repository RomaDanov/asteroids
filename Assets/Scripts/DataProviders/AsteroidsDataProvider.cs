using Configs.Asteriods;
using DataProviders;
using System.Linq;

namespace DataProviders
{
	public class AsteroidsDataProvider : DataProvider<AsteroidsLibraryConfig>
	{
		public AsteroidConfig GetAsteroidConfig(string id)
		{
			AsteroidConfig config = library.Items.FirstOrDefault(x => x.Id == id);
			return config;
		}
	}
}
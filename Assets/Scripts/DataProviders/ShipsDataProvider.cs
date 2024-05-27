using Configs.Ships;
using DataProviders;
using System.Linq;

namespace DataProviders
{
	public class ShipsDataProvider : DataProvider<ShipsLibraryConfig>
	{
		public ShipConfig GetShipConfig(string id)
		{
			ShipConfig shipConfig = library.Items.FirstOrDefault(x => x.Id == id);
			return shipConfig;
		}
	}
}
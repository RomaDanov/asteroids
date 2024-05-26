using Configs.Ships;
using System.Linq;

public class ShipsDataProvider : DataProvider<ShipsLibraryConfig>
{
	public ShipConfig GetShipConfig(string id)
	{
		ShipConfig shipConfig = library.Items.FirstOrDefault(x => x.Id == id);
		return shipConfig;
	}
}

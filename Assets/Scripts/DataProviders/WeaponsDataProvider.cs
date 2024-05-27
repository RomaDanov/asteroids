using Configs.Weapons;
using System.Linq;

namespace DataProviders
{
	public class WeaponsDataProvider : DataProvider<WeaponsLibraryConfig>
	{
		public WeaponConfig GetWeaponConfig(string id)
		{
			WeaponConfig config = library.Items.FirstOrDefault(x => x.Id == id);
			return config;
		}
	}
}
using Configs.Asteriods;
using ObjectPool;

namespace Contexts.Game.Components.Asteroid
{
	public class Asteroid : PoolableObject<Asteroid>
	{
		private AsteroidConfig config;

		public void Configure(AsteroidConfig config)
		{
			this.config = config;
		}
	}
}
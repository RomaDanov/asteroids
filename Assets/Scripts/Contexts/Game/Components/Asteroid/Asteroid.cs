using Configs.Asteriods;
using ObjectPool;

public class Asteroid : PoolableObject<Asteroid>
{
	private AsteroidConfig config;

	public void Configure(AsteroidConfig config)
	{
		this.config = config;
	}
}

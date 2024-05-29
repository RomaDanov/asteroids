using Configs.Asteriods;
using ObjectPool;
using UnityEngine;

public class Asteroid : PoolableObject<Asteroid>
{
	[SerializeField] private AsteroidMovement movement;

	public void Configure(AsteroidConfig config)
	{
		movement.Configure(config.MoveSpeedRange, config.RotateSpeedRange);
	}
}

using Configs.Asteriods;
using Contexts.Game.Components.Movement;
using Contexts.Game.Factories;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public class AsteroidSpawner : Spawner
	{
		[SerializeField] private float forceAngle;

		public override GameObject Spawn()
		{
			AsteroidsCreator creator = new AsteroidsCreator();
			AsteroidConfig asteroidConfig = config as AsteroidConfig;

			Asteroid.Asteroid asteroid = creator.Create(asteroidConfig, transform);
			IMovable asteroidMovable = asteroid.GetComponent<IMovable>();

			Vector2 force = transform.up * asteroidConfig.MoveSpeed;
			asteroidMovable.ApplyForce(force);

			return asteroid.gameObject;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!enabled) return;
			//TODO:
		}
#endif
	}
}
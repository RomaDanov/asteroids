using Configs;
using Configs.Asteriods;
using Contexts.Game.Components.Movements;
using Contexts.Game.Factories;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public class AsteroidSpawner : Spawner
	{
		[Space]
		[Header("Asteroid")]
		[SerializeField] private float forceAngle;

		public override GameObject Spawn(Config config, Transform parent)
		{
			AsteroidsCreator creator = new AsteroidsCreator();
			AsteroidConfig asteroidConfig = config as AsteroidConfig;

			Asteroid.Asteroid asteroid = creator.Create(asteroidConfig, parent);
			asteroid.transform.position = GetRandomSpawnPosition();
			IMovable asteroidMovable = asteroid.GetComponent<IMovable>();

			Vector2 position = GetPoint(Random.Range(-forceAngle, forceAngle));
			Vector2 force = position * asteroidConfig.MoveSpeed;
			asteroidMovable.ApplyForce(force);

			return asteroid.gameObject;
		}

		private Vector2 GetPoint(float angle)
		{
			Quaternion randomAngle = Quaternion.Euler(0, angle, 0);
			randomAngle = transform.rotation * randomAngle;
			Vector2 position = transform.up - randomAngle * Vector3.forward;
			return position;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!enabled) return;

			Gizmos.DrawLine(transform.position, transform.position + (Vector3)GetPoint(-forceAngle));
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)GetPoint(forceAngle));
		}
#endif
	}
}
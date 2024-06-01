using Configs.Asteriods;
using Contexts.Game.Components.Movement;
using Contexts.Game.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Contexts.Game.Components.Asteroid
{
	public class AsteroidFragmentsSpawner : MonoBehaviour
	{
		private IReadOnlyCollection<AsteroidConfig> destructionFragments;

		public void Configure(IReadOnlyCollection<AsteroidConfig> destructionFragments)
		{
			this.destructionFragments = destructionFragments;
		}

		public bool TrySpawnFragments()
		{
			bool success = false;

			AsteroidsCreator creator = new AsteroidsCreator();
			for (int i = 0; i < destructionFragments.Count; i++)
			{
				AsteroidConfig newConfig = destructionFragments.ElementAt(i);

				Vector2 insideUnitCircle = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * 0.5f;
				Vector2 position = (Vector2)transform.position + insideUnitCircle;
				Asteroid newAsteroid = creator.Create(newConfig, position);

				IMovable newMovable = newAsteroid.GetComponent<IMovable>();

				Vector2 direction = Vector2.zero;
				direction.x = Random.Range(-1f, 1f);
				direction.y = Random.Range(-1f, 1f);

				float speed = Random.Range(-newConfig.MoveSpeed, newConfig.MoveSpeed);

				Vector2 force = direction * speed;
				newMovable.ApplyForce(force);
				success = true;
			}

			return success;
		}
	}
}
using Configs.Asteriods;
using Contexts.Game.Components.Movements;
using Contexts.Game.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Contexts.Game.Components.Asteroid
{
	public class AsteroidFragmentsSpawner : MonoBehaviour
	{
		private IReadOnlyCollection<AsteroidConfig> destructionFragments;
		private int maxCount;

		public void Configure(IReadOnlyCollection<AsteroidConfig> destructionFragments, int maxCount)
		{
			this.destructionFragments = destructionFragments;
			this.maxCount = maxCount;
		}

		public bool TrySpawnFragments()
		{
			if (destructionFragments.Count == 0 || maxCount <= 0) return false;

			AsteroidsCreator creator = new AsteroidsCreator();
			for (int i = 0; i < maxCount; i++)
			{
				AsteroidConfig newConfig = destructionFragments.ElementAt(Random.Range(0, destructionFragments.Count));

				Vector2 insideUnitCircle = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * 0.3f;
				Vector2 position = (Vector2)transform.position + insideUnitCircle;
				Asteroid newAsteroid = creator.Create(newConfig, position);

				IMovable newMovable = newAsteroid.GetComponent<IMovable>();

				Vector2 force = Random.insideUnitCircle * newConfig.MoveSpeed;
				newMovable.ApplyForce(force);
			}
			return true;
		}
	}
}
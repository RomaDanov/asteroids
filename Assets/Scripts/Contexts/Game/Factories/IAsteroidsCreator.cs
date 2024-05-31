using Configs.Asteriods;
using Contexts.Game.Components.Asteroid;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public interface IAsteroidsCreator
	{
		Asteroid Create(string id, Vector3 worldPosition);
		Asteroid Create(AsteroidConfig config, Vector3 worldPosition);
	}
}
using Configs.Asteriods;
using UnityEngine;

public interface IAsteroidsCreator
{
	Asteroid Create(string id, Transform parent);
	Asteroid Create(AsteroidConfig config, Transform parent);
}

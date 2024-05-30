using Configs.Asteriods;
using Contexts.Game.Components.Asteroid;

namespace Contexts.Game.Factories
{
	public interface IAsteroidsCreator
	{
		Asteroid Create(string id);
		Asteroid Create(AsteroidConfig config);
	}
}
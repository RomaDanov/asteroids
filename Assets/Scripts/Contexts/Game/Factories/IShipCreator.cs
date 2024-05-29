using Configs.Ships;
using UnityEngine;

namespace Contexts.Game.Factories
{
	public interface IShipCreator
	{
		ShipView Create(string id, Transform parent);
		ShipView Create(ShipConfig config, Transform parent);
	}
}
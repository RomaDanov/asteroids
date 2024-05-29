using Configs.Ships;
using UnityEngine;

public interface IShipCreator
{
	ShipView Create(string id, Transform parent, Transform controllTransform);
	ShipView Create(ShipConfig config, Transform parent, Transform controllTransform);
}

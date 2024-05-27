using Configs.Ships;
using UnityEngine;

public interface IShipCreator
{
	ShipController Create(string id, Transform parent, Transform controllTransform);
	ShipController Create(ShipConfig config, Transform parent, Transform controllTransform);
}

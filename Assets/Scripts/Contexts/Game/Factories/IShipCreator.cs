using Configs.Ships;
using UnityEngine;

public interface IShipCreator
{
	ShipView Create(string id, Transform parent);
	ShipView Create(ShipConfig config, Transform parent);
}

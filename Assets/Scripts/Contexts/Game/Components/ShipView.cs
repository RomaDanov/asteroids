using ObjectPool;
using UnityEngine;

public class ShipView : PoolableObject<ShipView>
{
	[SerializeField] private ParticleSystem[] engineFXs;

	public void SetActiveEngineFX(bool isActive)
	{
		for (int i = 0; i < engineFXs.Length; i++)
		{
			if ((isActive && engineFXs[i].isPlaying) || (!isActive && !engineFXs[i].isPlaying)) continue;

			if(isActive) engineFXs[i].Play();
			else engineFXs[i].Stop();
		}
	}
}

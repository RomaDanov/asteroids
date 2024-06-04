using Contexts.Game.Components.Movement;
using ObjectPool;
using UnityEngine;

public class ShipView : PoolableObject<ShipView>
{
	[SerializeField] private ParticleSystem[] engineFXs;

	private IMovable movable;

	public void Configure(IMovable movable)
	{
		this.movable = movable;
		StopEngineFX();
	}

	private void FixedUpdate()
	{
		if (movable.State == IMovable.AccelerationState.INCREASE)
		{
			PlayEngineFX();
		}
		else if(movable.State == IMovable.AccelerationState.DECREASE)
		{
			StopEngineFX();
		}
	}

	private void PlayEngineFX()
	{
		for (int i = 0; i < engineFXs.Length; i++)
		{
			if (engineFXs[i].isPlaying) continue;

			engineFXs[i].Play();
		}
	}

	private void StopEngineFX()
	{
		for (int i = 0; i < engineFXs.Length; i++)
		{
			if (!engineFXs[i].isPlaying) continue;

			engineFXs[i].Stop();
		}
	}
}

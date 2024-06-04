using Contexts.Game.Components.Movement;
using ObjectPool;
using UnityEngine;

public class ShipView : PoolableObject<ShipView>
{
	[SerializeField] private ParticleSystem[] engineFXs;

	private IMovable movable;
	private Vector2 prevAcceleration;

	public void Configure(IMovable movable)
	{
		this.movable = movable;
		StopEngineFX();
	}

	private void FixedUpdate()
	{
		float current = movable.Acceleration.magnitude;
		float previous = prevAcceleration.magnitude;

		if (current > previous && !Mathf.Approximately(current, previous))
		{
			PlayEngineFX();
		}
		else if(current < previous && !Mathf.Approximately(current, previous))
		{
			StopEngineFX();
		}

		prevAcceleration = movable.Acceleration;
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

using UnityEngine;

namespace Architecture.Inputs
{
	public interface IInput
	{
		Vector2 GetAxis();
		bool GetFire();
		bool GetAlternativeFire();
		bool GetPause();
	}
}
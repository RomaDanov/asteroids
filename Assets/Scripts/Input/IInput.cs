namespace Inputs
{
	public interface IInput
	{
		float GetAccelerate();
		float GetRotation();
		bool GetFire();
		bool GetAlternativeFire();
	}
}
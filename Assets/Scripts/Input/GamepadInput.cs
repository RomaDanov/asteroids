using UnityEngine.InputSystem;

namespace Inputs
{
	public class GamepadInput : IInput
	{
		public float GetAccelerate()
		{
			return Gamepad.current.rightStick.ReadValue().y;
		}

		public bool GetAlternativeFire()
		{
			return Gamepad.current.bButton.IsPressed();
		}

		public bool GetFire()
		{
			return Gamepad.current.aButton.IsPressed();
		}

		public float GetRotation()
		{
			return Gamepad.current.leftStick.ReadValue().x;
		}
	}
}
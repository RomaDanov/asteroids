using UnityEngine.InputSystem;

namespace Inputs
{
	public class GamepadInput : IInput
	{
		public float GetAccelerate() => Gamepad.current.leftStick.ReadValue().y;
		public bool GetAlternativeFire() => Gamepad.current.bButton.IsPressed();
		public bool GetFire() => Gamepad.current.aButton.IsPressed();
		public bool GetPause() => Gamepad.current.startButton.wasPressedThisFrame;
		public float GetRotation() => Gamepad.current.leftStick.ReadValue().x;
	}
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
	public class GamepadInput : IInput
	{
		public Vector2 GetAxis() => Gamepad.current.leftStick.ReadValue();
		public bool GetAlternativeFire() => Gamepad.current.bButton.IsPressed();
		public bool GetFire() => Gamepad.current.aButton.IsPressed();
		public bool GetPause() => Gamepad.current.startButton.wasPressedThisFrame;
	}
}
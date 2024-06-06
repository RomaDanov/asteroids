using UnityEngine;
using UnityEngine.InputSystem;

namespace Architecture.Inputs
{
	public class GamepadInput : IInput
	{
		public enum GamepadButtonType
		{
			A,
			B,
			START
		}

		public Vector2 GetAxis() => Gamepad.current.leftStick.ReadValue();
		public bool GetAlternativeFire() => Gamepad.current.bButton.IsPressed();
		public bool GetFire() => Gamepad.current.aButton.IsPressed();
		public bool GetPause() => Gamepad.current.startButton.wasPressedThisFrame;
	}
}
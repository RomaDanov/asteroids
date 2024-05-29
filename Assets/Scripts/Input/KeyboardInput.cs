using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
	public class KeyboardInput : IInput
	{
		public Vector2 GetAxis()
		{
			Vector2 axis = Vector2.zero;
			if (Keyboard.current.wKey.IsPressed() || Keyboard.current.upArrowKey.IsPressed())
			{
				axis.y = 1;
			}
			else if (Keyboard.current.sKey.IsPressed() || Keyboard.current.downArrowKey.IsPressed())
			{
				axis.y = -1;
			}

			if (Keyboard.current.aKey.IsPressed() || Keyboard.current.leftArrowKey.IsPressed())
			{
				axis.x = -1;
			}
			else if (Keyboard.current.dKey.IsPressed() || Keyboard.current.rightArrowKey.IsPressed())
			{
				axis.x = 1;
			}

			return axis;
		}

		public bool GetAlternativeFire() => Keyboard.current.leftAltKey.IsPressed();
		public bool GetFire() => Keyboard.current.spaceKey.IsPressed();
		public bool GetPause() => Keyboard.current.escapeKey.wasPressedThisFrame;
	}
}
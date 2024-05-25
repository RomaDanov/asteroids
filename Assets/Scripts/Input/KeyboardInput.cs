using UnityEngine.InputSystem;

namespace Inputs
{
	public class KeyboardInput : IInput
	{
		public float GetAccelerate()
		{
			return Keyboard.current.wKey.IsPressed() || Keyboard.current.upArrowKey.IsPressed() ? 1 : 0;
		}

		public bool GetAlternativeFire()
		{
			return Keyboard.current.leftAltKey.IsPressed();
		}

		public bool GetFire()
		{
			return Keyboard.current.spaceKey.IsPressed();
		}

		public float GetRotation()
		{
			float direction = 0f;
			if (Keyboard.current.aKey.IsPressed() || Keyboard.current.leftArrowKey.IsPressed())
			{
				direction = -1;
			}
			else if (Keyboard.current.dKey.IsPressed() || Keyboard.current.rightArrowKey.IsPressed())
			{
				direction = 1;
			}
			return direction;
		}
	}
}
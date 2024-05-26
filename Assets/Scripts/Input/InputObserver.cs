using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Inputs
{
	public class InputObserver : IDisposable
	{
		public event Action<InputType> InputTypeChanged;

		public InputType InputType { get; private set; }

		public void Initialize()
		{
			PlayerLoopUtils.RegisterUpdateFunction(UpdateInput);
		}

		public void Dispose()
		{
			PlayerLoopUtils.UnregisterUpdateFunction(UpdateInput);
		}

		private void UpdateInput()
		{
			InputType inputType = InputType;
			if (IsGamepad())
			{
				inputType = InputType.GAMEPAD;
			}
			else if (IsKeyboardMouse())
			{
				inputType = InputType.KEYBOARD_MOUSE;
			}
			SetInputType(inputType);
		}

		private bool IsKeyboardMouse()
		{
			bool isKeyboard = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
			bool isMouse = Mouse.current != null &&
				(Mouse.current.delta.ReadValue().magnitude != 0.0f ||
				Mouse.current.scroll.ReadValue().magnitude != 0.0f ||
				Mouse.current.leftButton.wasPressedThisFrame ||
				Mouse.current.rightButton.wasPressedThisFrame);

			return isKeyboard || isMouse;
		}

		private bool IsGamepad()
		{
			bool isGamepad = Gamepad.current != null &&
				(Gamepad.current.leftStick.ReadValue().magnitude != 0.0f ||
				Gamepad.current.rightStick.ReadValue().magnitude != 0.0f ||
				Gamepad.current.leftTrigger.ReadValue() != 0.0f ||
				Gamepad.current.rightTrigger.ReadValue() != 0.0f ||
				Gamepad.current.allControls.Any(x => x is ButtonControl && x.IsPressed() && !x.synthetic));

			return isGamepad;
		}

		private void SetInputType(InputType type)
		{
			if (InputType == type) return;

			InputType = type;
			InputTypeChanged?.Invoke(InputType);
		}
	}
}
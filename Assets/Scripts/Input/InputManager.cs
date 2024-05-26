using Singleton;
using System;

namespace Inputs
{
	public class InputManager : SingletonInstance<InputManager>, IDisposable
	{
		private InputObserver observer;

		public IInput Input { get; private set; }
		public InputType CurrentType => observer.InputType;

		public InputManager()
		{
			observer = new InputObserver();
			observer.InputTypeChanged += OnInputTypeChanged;
			observer.Initialize();

			OnInputTypeChanged(observer.InputType);
		}

		public void Dispose()
		{
			observer.Dispose();
			observer.InputTypeChanged -= OnInputTypeChanged;
		}

		private void OnInputTypeChanged(InputType inputType)
		{
			switch (inputType)
			{
				case InputType.KEYBOARD_MOUSE:
					Input = new KeyboardInput();
					break;
				case InputType.GAMEPAD:
					Input = new GamepadInput();
					break;
			}
		}
	}
}
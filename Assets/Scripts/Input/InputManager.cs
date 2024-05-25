using Singleton;
using System;
using UnityEngine;

namespace Inputs
{
	public class InputManager : SingletonInstance<InputManager>, IDisposable
	{
		private InputObserver observer;

		public IInput Input { get; private set; }
		public InputType CurrentType => observer.InputType;

		public InputManager()
		{
			observer = new GameObject($"[{nameof(InputObserver)}]").AddComponent<InputObserver>();
			observer.InputTypeChanged += OnInputTypeChanged;
			OnInputTypeChanged(observer.InputType);
			GameObject.DontDestroyOnLoad(observer);
		}

		public void Dispose()
		{
			observer.InputTypeChanged -= OnInputTypeChanged;
			GameObject.Destroy(observer.gameObject);
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
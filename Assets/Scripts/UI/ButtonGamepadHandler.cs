using Architecture.Inputs;
using Architecture.ServiceLocator;
using DataProviders;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Architecture.Inputs.GamepadInput;

[RequireComponent(typeof(Button))]
public class ButtonGamepadHandler : MonoBehaviour
{
	[SerializeField] private GamepadButtonType gamepadButton;
	[SerializeField] private Button button;
	[SerializeField] private Image gamepadImage;

	private void Start()
	{
		CommonDataProvider commonDataProvider = ServicesManager.Instance.Get<CommonDataProvider>();
		gamepadImage.sprite = commonDataProvider.GetGamepadButtonIcon(gamepadButton);
	}

	private void OnEnable()
	{
		InputManager.Instance.InputChanged += OnInputChanged;
		OnInputChanged(InputManager.Instance.CurrentType);
	}

	private void Update()
	{
		if ((Gamepad.current.aButton.wasPressedThisFrame && gamepadButton == GamepadButtonType.A) ||
			(Gamepad.current.bButton.wasPressedThisFrame && gamepadButton == GamepadButtonType.B) ||
			(Gamepad.current.startButton.wasPressedThisFrame && gamepadButton == GamepadButtonType.START))
		{
			button.onClick.Invoke();
		}
	}

	private void OnDisable()
	{
		InputManager.Instance.InputChanged -= OnInputChanged;
	}

	private void OnInputChanged(InputType inputType)
	{
		gamepadImage.gameObject.SetActive(inputType == InputType.GAMEPAD);
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		if (button != null) return;
		button = GetComponent<Button>();
	}
#endif
}

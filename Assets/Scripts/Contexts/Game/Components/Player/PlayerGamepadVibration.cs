using Architecture.Inputs;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Contexts.Game.Components.Player
{
	public class PlayerGamepadVibration : MonoBehaviour
	{
		[SerializeField] private Health health;

		private void Start()
		{
			health.DamageTaken += OnDamageTaken;
		}

		private void OnDestroy()
		{
			health.DamageTaken -= OnDamageTaken;
			Dispose();
		}

		private void Dispose()
		{
			if (InputManager.Instance.CurrentType == InputType.GAMEPAD)
			{
				Gamepad.current.SetMotorSpeeds(0, 0);
			}
		}

		private void OnDamageTaken(float amount)
		{
			if (InputManager.Instance.CurrentType == InputType.GAMEPAD)
			{
				Vibrate(0.1f);
			}
		}

		private void Vibrate(float duration)
		{
			Gamepad.current.SetMotorSpeeds(0, 1);
			StartCoroutine(StopVibrate(duration));
		}
		private IEnumerator StopVibrate(float duration)
		{
			float currentTime = 0;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				yield return null;
			}
			Gamepad.current.SetMotorSpeeds(0, 0);
		}
	}
}
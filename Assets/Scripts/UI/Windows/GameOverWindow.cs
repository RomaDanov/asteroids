using Architecture.Messages;
using Architecture.WindowManagment;
using Contexts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class GameOverWindow : Window
	{
		[SerializeField] private Button restartButton;
		[SerializeField] private Button exitButton;

		protected override void OnWindowOpened()
		{
			restartButton.onClick.AddListener(OnRestart);
			exitButton.onClick.AddListener(OnExit);
		}

		protected override void OnWindowClosed()
		{
			restartButton.onClick.RemoveListener(OnRestart);
			exitButton.onClick.RemoveListener(OnExit);
		}

		private void OnRestart()
		{
			MessageRouter.Instance.Publish<GameMessages.RestartGameMessage>();
		}

		private void OnExit()
		{
			MessageRouter.Instance.Publish<GameMessages.ExitGameMessage>();
		}
	}
}
using Architecture.Messages;
using Architecture.WindowManagment;
using Contexts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class PauseWindow : Window
	{
		[SerializeField] private Button closeButton;
		[SerializeField] private Button exitButton;

		protected override void OnWindowOpened()
		{
			closeButton.onClick.AddListener(OnClose);
			exitButton.onClick.AddListener(OnExit);
		}

		protected override void OnWindowClosed()
		{
			closeButton.onClick.RemoveListener(OnClose);
			exitButton.onClick.RemoveListener(OnExit);

			MessageRouter.Instance.Publish<GameMessages.UnpauseGameMessage>();
		}

		private void OnClose()
		{
			Close();
		}

		private void OnExit()
		{
			MessageRouter.Instance.Publish<GameMessages.ExitGameMessage>();
		}
	}
}
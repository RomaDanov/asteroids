using Architecture.Messages;
using Architecture.WindowManagment;
using Contexts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class GameHUD : Window
	{
		[SerializeField] private Button pauseButton;

		protected override void OnWindowOpened()
		{
			pauseButton.onClick.AddListener(OnPause);
		}

		protected override void OnWindowClosed()
		{
			pauseButton.onClick.RemoveListener(OnPause);
		}

		private void OnPause()
		{
			MessageRouter.Instance.Publish<GameMessages.PauseGameMessage>();
		}
	}
}
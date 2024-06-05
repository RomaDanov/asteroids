using Architecture.Inputs;
using Architecture.Messages;
using Architecture.StateMachine;
using Architecture.WindowManagment;
using UI.Windows;

namespace Contexts.Game.States
{
	public class GameMainLoopState : State
	{
		private GameHUD gameHUDWindow;

		internal override void Awake()
		{
			gameHUDWindow = WindowManager.Instance.GetWindow<GameHUD>();
		}

		internal override void Enter()
		{
			gameHUDWindow?.Open();

			MessageRouter.Instance.Subscribe<GameMessages.PauseGameMessage>(OnPauseGame);
			MessageRouter.Instance.Subscribe<GameMessages.PlayerDiedMessage>(OnPlayerDied);
		}

		internal override void Update()
		{
			if (InputManager.Instance.Input.GetPause())
			{
				Finish<GamePauseState>();
			}
		}

		internal override void Exit()
		{
			gameHUDWindow?.Close();

			MessageRouter.Instance.Unsubscribe<GameMessages.PauseGameMessage>(OnPauseGame);
			MessageRouter.Instance.Unsubscribe<GameMessages.PlayerDiedMessage>(OnPlayerDied);
		}

		private void OnPauseGame(GameMessages.PauseGameMessage message)
		{
			Finish<GamePauseState>();
		}

		private void OnPlayerDied(GameMessages.PlayerDiedMessage message)
		{
			Finish<GameOverState>();
		}
	}
}
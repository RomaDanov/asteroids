using Architecture.Inputs;
using Architecture.Messages;
using Architecture.StateMachine;
using Architecture.WindowManagment;
using UI.Windows;
using UnityEngine;

namespace Contexts.Game.States
{
	public class GamePauseState : State
	{
		private PauseWindow pauseWindow;
		private float originTimeScale;

		internal override void Awake()
		{
			pauseWindow = WindowManager.Instance.GetWindow<PauseWindow>();
			originTimeScale = Time.timeScale;
		}

		internal override void Enter()
		{
			MessageRouter.Instance.Subscribe<GameMessages.UnpauseGameMessage>(OnUnpausedGame);
			MessageRouter.Instance.Subscribe<GameMessages.ExitGameMessage>(OnExitGame);

			pauseWindow?.Open();

			MessageRouter.Instance.Publish(new GameMessages.GamePausedMessage());

			Time.timeScale = 0;
		}

		internal override void Update()
		{
			if (InputManager.Instance.Input.GetPause())
			{
				Return();
			}
		}

		internal override void Exit()
		{
			MessageRouter.Instance.Unsubscribe<GameMessages.UnpauseGameMessage>(OnUnpausedGame);

			pauseWindow?.Close();

			Time.timeScale = originTimeScale;
			MessageRouter.Instance.Publish(new GameMessages.GameUnpausedMessage());
		}

		private void OnUnpausedGame(GameMessages.UnpauseGameMessage message)
		{
			Return();
		}

		private void OnExitGame(GameMessages.ExitGameMessage message)
		{
			Finish<GameExitState>();
		}
	}
}
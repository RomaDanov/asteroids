using Architecture.Messages;
using Architecture.StateMachine;
using Architecture.WindowManagment;
using System.Threading.Tasks;
using UI.Windows;
using UnityEngine.SceneManagement;

namespace Contexts.Game.States
{
	public class GameOverState : State
	{
		private GameOverWindow gameOverWindow;

		internal override void Awake()
		{
			gameOverWindow = WindowManager.Instance.GetWindow<GameOverWindow>();
		}

		internal override void Enter()
		{
			MessageRouter.Instance.Subscribe<GameMessages.ExitGameMessage>(OnExitGame);
			MessageRouter.Instance.Subscribe<GameMessages.RestartGameMessage>(OnRestartGame);

			gameOverWindow?.Open();
		}

		internal override void Exit()
		{
			MessageRouter.Instance.Unsubscribe<GameMessages.ExitGameMessage>(OnExitGame);
			MessageRouter.Instance.Unsubscribe<GameMessages.RestartGameMessage>(OnRestartGame);

			gameOverWindow?.Close();
		}

		private async Task RestartGameAsync()
		{
			string activeSceneName = SceneManager.GetActiveScene().name;

			var unloadAsyncOp = SceneManager.UnloadSceneAsync(activeSceneName);
			while (!unloadAsyncOp.isDone)
			{
				await Task.Yield();
			}

			var loadAsyncOp = SceneManager.LoadSceneAsync(activeSceneName, LoadSceneMode.Single);
			while (!loadAsyncOp.isDone)
			{
				await Task.Yield();
			}
		}

		private void OnRestartGame(GameMessages.RestartGameMessage message)
		{
			_ = RestartGameAsync();
		}

		private void OnExitGame(GameMessages.ExitGameMessage message)
		{
			Finish<GameExitState>();
		}
	}
}
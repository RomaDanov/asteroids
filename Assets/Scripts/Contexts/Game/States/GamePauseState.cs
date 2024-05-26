using Inputs;
using Messages;
using StateMachine;
using UnityEngine;

namespace Contexts.Game.States
{
	public class GamePauseState : State
	{
		private float originTimeScale;

		internal override void Awake()
		{
			originTimeScale = Time.timeScale;
		}

		internal override void Enter()
		{
			MessageRouter.Instance.Subscribe<GameMessages.UnpauseGameMessage>(OnUnpausedGame);

			Time.timeScale = 0;
			MessageRouter.Instance.Publish(new GameMessages.GamePausedMessage());

			Debug.Log("Enter: PauseState");
		}

		internal override void Update()
		{
			if (InputManager.Instance.Input.GetPause())
			{
				Finish<GameMainLoopState>();
			}
		}

		internal override void Exit()
		{
			MessageRouter.Instance.Unsubscribe<GameMessages.UnpauseGameMessage>(OnUnpausedGame);

			Time.timeScale = originTimeScale;
			MessageRouter.Instance.Publish(new GameMessages.GameUnpausedMessage());
		}

		private void OnUnpausedGame(GameMessages.UnpauseGameMessage message)
		{
			Finish<GameMainLoopState>();
		}
	}
}
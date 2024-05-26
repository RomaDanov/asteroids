using Inputs;
using Messages;
using StateMachine;
using UnityEngine;

namespace Contexts.Game.States
{
	public class GameMainLoopState : State
	{
		internal override void Enter()
		{
			MessageRouter.Instance.Subscribe<GameMessages.PauseGameMessage>(OnPauseGame);

			Debug.Log("Enter: MainLoopState");
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
			MessageRouter.Instance.Unsubscribe<GameMessages.PauseGameMessage>(OnPauseGame);
		}

		private void OnPauseGame(GameMessages.PauseGameMessage message)
		{
			Finish<GamePauseState>();
		}
	}
}
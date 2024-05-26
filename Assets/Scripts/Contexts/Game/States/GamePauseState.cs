using Inputs;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Contexts.Game.States
{
	public class GamePauseState : State
	{
		internal override void Enter()
		{
			Time.timeScale = 0;
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
			Time.timeScale = 1;
		}
	}
}
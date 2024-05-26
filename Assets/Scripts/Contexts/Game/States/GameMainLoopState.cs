using Inputs;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Contexts.Game.States
{
	public class GameMainLoopState : State
	{
		internal override void Enter()
		{
			Debug.Log("Enter: MainLoopState");
		}

		internal override void Update()
		{
			if (InputManager.Instance.Input.GetPause())
			{
				Finish<GamePauseState>();
			}
		}
	}
}
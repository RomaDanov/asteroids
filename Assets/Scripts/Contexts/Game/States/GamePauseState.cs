using Inputs;
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
			Time.timeScale = originTimeScale;
		}
	}
}
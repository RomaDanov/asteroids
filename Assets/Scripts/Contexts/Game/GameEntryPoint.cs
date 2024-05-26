using Contexts.Game.States;
using Inputs;
using Messages;
using ObjectPool;
using StateMachine;
using UnityEngine;

namespace Contexts.Game
{
	public class GameEntryPoint : MonoBehaviour
	{
		private StateMachine.StateMachineBehaviour stateMachine;

		private void Start()
		{
			stateMachine = BuildStateMachine();
			stateMachine.Start<GameEntryState>();
		}

		private void Update()
		{
			stateMachine?.Update();
		}

		private void OnDestroy()
		{
			stateMachine?.Stop();
			stateMachine?.Dispose();

			InputManager.Instance?.Dispose();
			MessageRouter.Instance?.Dispose();
			ObjectPoolService.Instance?.Dispose();
		}

		private StateMachine.StateMachineBehaviour BuildStateMachine()
		{
			return new StateMachineBuilder("GameStateMachine")
				.State<GameEntryState>()
				.State<GameMainLoopState>()
				.State<GamePauseState>()
				.State<GameExitState>()
				.Build();
		}
	}
}
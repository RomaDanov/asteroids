using Contexts.Game.States;
using Inputs;
using Messages;
using ObjectPool;
using ServiceLocator;
using StateMachine;
using UnityEngine;

namespace Contexts.Game
{
	public class GameEntryPoint : MonoBehaviour
	{
		private StateMachine.StateMachineBehaviour stateMachine;

		private void Start()
		{
			ServicesManager.Instance.Register<ShipsDataProvider>();
			ServicesManager.Instance.Register<EnemiesDataProvider>();
			ServicesManager.Instance.Register<AsteroidsDataProvider>();
			ServicesManager.Instance.InitializeServices();

			stateMachine = new StateMachineBuilder("GameStateMachine")
				.State<GameEntryState>()
				.State<GameMainLoopState>()
				.State<GamePauseState>()
				.State<GameExitState>()
				.Build();

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

			ServicesManager.Instance.Dispose();
		}
	}
}
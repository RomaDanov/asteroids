using Contexts.Game.Components.Asteroid;
using Contexts.Game.Components.Movement;
using Contexts.Game.Components.Player;
using Contexts.Game.Factories;
using Contexts.Game.States;
using DataProviders;
using Inputs;
using ServiceLocator;
using StateMachine;
using UnityEngine;

namespace Contexts.Game
{
	public class GameEntryPoint : MonoBehaviour
	{
		[SerializeField] private Player player;

		private StateMachine.StateMachineBehaviour stateMachine;

		#region Unity Methods
		private void Awake()
		{
			InitializeServices();
			InitializeStateMachine();

			//TEST
			AsteroidsCreator creator = new AsteroidsCreator();
			Asteroid asteroid = creator.Create("ASTEROID_BIG");
			Vector3 pos = asteroid.transform.position;
			pos.x += 3;
			pos.y += 3;
			asteroid.transform.position = pos;
			//
		}

		private void Update()
		{
			stateMachine?.Update();
		}

		private void OnDestroy()
		{
			DisposeStateMachine();
			DisposeService();
		}
		#endregion

		#region Initialize
		private void InitializeServices()
		{
			ServicesManager.Instance.Register<CommonDataProvider>();
			ServicesManager.Instance.Register<ShipsDataProvider>();
			ServicesManager.Instance.Register<EnemiesDataProvider>();
			ServicesManager.Instance.Register<AsteroidsDataProvider>();
			ServicesManager.Instance.Register<WeaponsDataProvider>();
			ServicesManager.Instance.InitializeServices();
		}

		private void InitializeStateMachine()
		{
			stateMachine = new StateMachineBuilder("GameStateMachine")
				.State(new GameEntryState(player))
				.State<GameMainLoopState>()
				.State<GamePauseState>()
				.State<GameExitState>()
				.Build();

			stateMachine.Start<GameEntryState>();
		}
		#endregion

		#region Dispose
		private void DisposeService()
		{
			InputManager.Instance?.Dispose();
			ServicesManager.Instance.Dispose();
		}

		private void DisposeStateMachine()
		{
			stateMachine?.Stop();
			stateMachine?.Dispose();
		}
		#endregion
	}
}
using Contexts.Game.Components.Asteroid;
using Contexts.Game.Components.Movement;
using Contexts.Game.Components.Player;
using Contexts.Game.Factories;
using Contexts.Game.States;
using DataProviders;
using Inputs;
using Messages;
using ServiceLocator;
using StateMachine;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameMessages;

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
			MessageRouter.Instance.Subscribe<AsteroidDestroyedMessage>(OnAsteroidDestroyed);
			//
		}

		private void Update()
		{
			//TEST
			if (Keyboard.current.oKey.wasPressedThisFrame)
			{
				OnAsteroidDestroyed(new AsteroidDestroyedMessage("ASTEROID_BIG"));
			}
			//

			stateMachine?.Update();
		}

		private void OnDestroy()
		{
			//TEST
			MessageRouter.Instance.Unsubscribe<AsteroidDestroyedMessage>(OnAsteroidDestroyed);
			//

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

		//TEST
		private void OnAsteroidDestroyed(AsteroidDestroyedMessage message)
		{
			if (message.Id != "ASTEROID_BIG") return;

			AsteroidsCreator creator = new AsteroidsCreator();
			Asteroid asteroid = creator.Create("ASTEROID_BIG", new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
		}
		//
	}
}
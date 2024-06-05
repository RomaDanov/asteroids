using Architecture.Inputs;
using Architecture.ServiceLocator;
using Architecture.StateMachine;
using Contexts.Game.Components.Player;
using Contexts.Game.States;
using DataProviders;
using UnityEngine;

namespace Contexts.Game
{
	public class GameEntryPoint : MonoBehaviour
	{
		[SerializeField] private Player player;

		private Architecture.StateMachine.StateMachineBehaviour stateMachine;

		#region Unity Methods
		private void Awake()
		{
			InitializeServices();

			SceneLoader uiLoader = new SceneLoader("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
			uiLoader.Loaded += OnUILoaded;
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
			InputManager.Instance.Initialize();

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
				.State<GameOverState>()
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

		private void OnUILoaded(string sceneName)
		{
			InitializeStateMachine();
		}
	}
}
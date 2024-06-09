using Architecture.ServiceLocator;
using Architecture.StateMachine;
using Architecture.WindowManagment;
using Contexts.Game.Components.Player;
using DataProviders;
using UI.Windows;

namespace Contexts.Game.States
{
	public class GameEntryState : State
	{
		private CommonDataProvider commonDataProvider;
		private MovablePanelsWindow movablePanelsWindow;
		private Player player;

		public GameEntryState(Player player)
		{
			this.player = player;
		}

		internal override void Awake()
		{
			commonDataProvider = ServicesManager.Instance.Get<CommonDataProvider>();
			movablePanelsWindow = WindowManager.Instance.GetWindow<MovablePanelsWindow>();
			movablePanelsWindow.Open();
		}

		internal override void Enter()
		{
			player.Configure(commonDataProvider.GetDefaultPlayerShip(), commonDataProvider.GetDefaultPlayerWeapons());
			Finish();
		}

		internal override void Dispose()
		{
			movablePanelsWindow.Close();
		}
	}
}
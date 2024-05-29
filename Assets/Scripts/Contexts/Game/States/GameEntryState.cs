using Contexts.Game.Components.Player;
using DataProviders;
using ServiceLocator;
using StateMachine;

namespace Contexts.Game.States
{
	public class GameEntryState : State
	{
		private CommonDataProvider commonDataProvider;
		private Player player;

		public GameEntryState(Player player)
		{
			this.player = player;
		}

		internal override void Awake()
		{
			commonDataProvider = ServicesManager.Instance.Get<CommonDataProvider>();
		}

		internal override void Enter()
		{
			player.Configure(commonDataProvider.GetDefaultPlayerShip(), commonDataProvider.GetDefaultPlayerWeapons());
			Finish();
		}
	}
}
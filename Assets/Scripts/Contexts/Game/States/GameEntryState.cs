using Configs.Ships;
using DataProviders;
using ServiceLocator;
using StateMachine;

namespace Contexts.Game.States
{
	public class GameEntryState : State
	{
		private ShipsDataProvider shipsDataProvider;

		private Player player;

		public GameEntryState(Player player)
		{
			this.player = player;
		}

		internal override void Awake()
		{
			shipsDataProvider = ServicesManager.Instance.Get<ShipsDataProvider>();
		}

		internal override void Enter()
		{
			ShipConfig ship = shipsDataProvider.GetShipConfig("SHIP_BLUE");
			player.Configure(ship);

			Finish();
		}
	}
}
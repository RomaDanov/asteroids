using Messages;

namespace Contexts.Game
{
	public class GameMessages
	{
		public struct PauseGameMessage : IMessage { }
		public struct GamePausedMessage : IMessage { }
		public struct UnpauseGameMessage : IMessage { }
		public struct GameUnpausedMessage : IMessage { }

		public struct ExitGameMessage : IMessage { }
	}
}
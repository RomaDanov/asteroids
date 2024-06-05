using Architecture.Messages;

namespace Contexts.Game
{
	public class GameMessages
	{
		public struct GamePausedMessage : IMessage { }
		public struct UnpauseGameMessage : IMessage { }
		public struct GameUnpausedMessage : IMessage { }

		public struct PauseGameMessage : IMessage { }
		public struct ExitGameMessage : IMessage { }
		public struct RestartGameMessage : IMessage { }

		public struct PlayerDiedMessage : IMessage { }
	}
}
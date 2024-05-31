using Messages;

public class GameMessages
{
	public struct PauseGameMessage : IMessage { }
	public struct GamePausedMessage : IMessage { }
	public struct UnpauseGameMessage : IMessage { }
	public struct GameUnpausedMessage : IMessage { }

	public struct ExitGameMessage : IMessage { }

	public struct AsteroidDestroyedMessage : IMessage 
	{
		public string Id;

		public AsteroidDestroyedMessage(string id)
		{
			Id = id;
		}
	}
}

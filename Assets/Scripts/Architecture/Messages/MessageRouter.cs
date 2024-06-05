using Singleton;
using System;
using System.Collections.Generic;

namespace Architecture.Messages
{
	public class MessageRouter : SingletonInstance<MessageRouter>, IDisposable
	{
		private readonly Dictionary<Type, MessageHandler> messages = new();

		public IReadOnlyDictionary<Type, MessageHandler> Messages => messages;

		public void Publish<TMessage>(TMessage message = default) where TMessage : struct, IMessage
		{
			var messageType = typeof(TMessage);

			if (messages.TryGetValue(messageType, out var handler))
			{
				handler.Publish(message);
			}
		}

		public void Subscribe<TMessage>(Action<TMessage> action) where TMessage : struct, IMessage
		{
			var messageType = typeof(TMessage);

			if (!messages.TryGetValue(messageType, out var handler))
			{
				handler = new MessageHandler();
				messages.Add(messageType, handler);
			}

			handler.Subscribe(action);
		}

		public void Unsubscribe<TMessage>(Action<TMessage> action) where TMessage : struct, IMessage
		{
			var messageType = typeof(TMessage);

			if (messages.TryGetValue(messageType, out var handler))
			{
				handler.Unsubscribe(action);

				if (handler.Subscriptions.Count == 0)
				{
					messages.Remove(messageType);
				}
			}
		}

		public void Dispose()
		{
			messages.Clear();
		}
	}
}
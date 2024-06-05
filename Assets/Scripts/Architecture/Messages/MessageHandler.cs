using System;
using System.Collections.Generic;

namespace Architecture.Messages
{
	public class MessageHandler
	{
		private List<Delegate> subscriptions = new();

		public IReadOnlyList<Delegate> Subscriptions => subscriptions;

		internal void Publish<TMessage>(TMessage message) where TMessage : struct, IMessage
		{
			for (var i = 0; i < subscriptions.Count; i++)
			{
				var action = subscriptions[i] as Action<TMessage>;
				action?.Invoke(message);
			}
		}

		internal void Subscribe(Delegate subscription)
		{
			subscriptions.Add(subscription);
		}

		internal void Unsubscribe(Delegate subscription)
		{
			subscriptions.Remove(subscription);
		}
	}
}
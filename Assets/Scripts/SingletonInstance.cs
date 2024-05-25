using System;

namespace Singleton
{
	public abstract class SingletonInstance<T> where T : class
	{
		private static T instance;

		public static T Instance
		{
			get
			{
				if (instance != null) return instance;

				instance = Activator.CreateInstance<T>();
				return instance;
			}
		}
	}
}
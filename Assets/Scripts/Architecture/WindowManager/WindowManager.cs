using Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.WindowManagment
{
	public class WindowManager : SingletonInstance<WindowManager>
	{
		private Dictionary<Type, Window> windows = new();

		public T GetWindow<T>() where T : Window
		{
			var windowType = typeof(T);
			if (windows.TryGetValue(windowType, out Window currentWindow))
			{
				return (T)currentWindow;
			}

			Debug.LogError($"Can't find window with type: {windowType.Name}");
			return null;
		}

		public void AddWindow<T>(T window) where T : Window
		{
			var windowType = window.GetType();
			if (!windows.TryGetValue(windowType, out Window currentWindow))
			{
				currentWindow = window;
				windows.Add(windowType, currentWindow);
			}
			else
			{
				Debug.LogError($"Window {windowType.Name} already added!");
			}
		}

		public void RemoveWindow<T>(T window) where T : Window
		{
			var windowType = window.GetType();
			if (windows.TryGetValue(windowType, out Window currentWindow))
			{
				windows.Remove(windowType);
			}
			else
			{
				Debug.LogError($"Window {windowType.Name} already removed!");
			}
		}
	}
}
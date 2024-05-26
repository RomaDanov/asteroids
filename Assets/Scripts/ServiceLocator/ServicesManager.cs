using Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator
{
	public class ServicesManager : SingletonInstance<ServicesManager>
	{
		private Dictionary<Type, ServiceInstance> services = new();

		public void Register<T>(T service) where T : ServiceInstance
		{
			var serviceType = typeof(T);
			if (services.TryGetValue(serviceType, out var registeredService))
			{
				Debug.LogError($"Service is already registered: {serviceType.Name}");
				return;
			}
			services.Add(serviceType, service);
		}

		public T Register<T>() where T : ServiceInstance, new()
		{
			T service = new T();
			Register(service);
			return service;
		}

		public void InitializeServices()
		{
			foreach (var kvp in services)
			{
				ServiceInstance service = kvp.Value;
				service.PreLoad();
			}

			foreach (var kvp in services)
			{
				ServiceInstance service = kvp.Value;
				service.Load();
			}

			foreach (var kvp in services)
			{
				ServiceInstance service = kvp.Value;
				service.PostLoad();
			}
		}

		public void Dispose()
		{
			foreach (var kvp in services)
			{
				ServiceInstance service = kvp.Value;
				service.Dispose();
			}
			services.Clear();
		}

		public void Unregister<T>() where T : ServiceInstance
		{
			var serviceType = typeof(T);
			if (!services.TryGetValue(serviceType, out var registeredService))
			{
				Debug.LogError($"Service is already unregistered: {serviceType.Name}");
				return;
			}
			registeredService.Dispose();
			services.Remove(serviceType);
		}

		public T Get<T>() where T : ServiceInstance
		{
			var serviceType = typeof(T);
			if (!services.TryGetValue(serviceType, out var registeredService))
			{
				Debug.LogError($"Service is not registered: {serviceType.Name}");
				return null;
			}
			return (T)registeredService;
		}

		public bool TryGet<T>(out T service) where T : ServiceInstance
		{
			service = Get<T>();
			return service != null;
		}
	}
}
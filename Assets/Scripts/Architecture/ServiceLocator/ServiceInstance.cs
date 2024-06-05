using System;

namespace Architecture.ServiceLocator
{
	public abstract class ServiceInstance : IDisposable
	{
		public virtual void PreLoad() { }
		public virtual void Load() { }
		public virtual void PostLoad() { }
		public virtual void Dispose() { }
	}
}
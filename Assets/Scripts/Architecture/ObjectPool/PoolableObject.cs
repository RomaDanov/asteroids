using UnityEngine;

namespace Architecture.ObjectPool
{
	public abstract class PoolableObject<T> : MonoBehaviour, IPoolableObject
	{
		public IObjectPool<T> Pool { get; private set; }

		public virtual void Init(IObjectPool<T> pool)
		{
			Pool = pool;
		}

		public virtual void OnGet() { }
		public virtual void OnRelease() { }
	}
}
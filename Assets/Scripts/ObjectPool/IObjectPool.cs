using System;
using UnityEngine;

namespace ObjectPool
{
	public interface IObjectPool : IDisposable
	{
		void Init(int prewarmLoad);
	}

	public interface IObjectPool<T> : IObjectPool
	{
		T Get(Transform root = null);
		void Release(T obj);
	}
}
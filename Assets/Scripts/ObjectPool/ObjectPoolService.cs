using Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
	public class ObjectPoolService : SingletonInstance<ObjectPoolService>
	{
		private const string ROOT_NAME = "[Object Pool]";

		private readonly Dictionary<Type, IObjectPool> pools = new();

		private Transform root;

		private void CreateRoot()
		{
			GameObject rootObject = new GameObject(ROOT_NAME);
			root = rootObject.transform;
			root.gameObject.SetActive(false);
			GameObject.DontDestroyOnLoad(rootObject);
		}

		public IObjectPool<T> GetPool<T>() where T : PoolableObject<T>, new()
		{
			var objType = typeof(T);
			if (!pools.TryGetValue(objType, out var objectPool))
			{
				return null;
			}

			return (IObjectPool<T>)objectPool;
		}

		public IObjectPool<T> GetOrCreatePool<T>(T obj, int prewarmLoad) where T : PoolableObject<T>, new()
		{
			var objType = typeof(T);
			if (!pools.TryGetValue(objType, out var objectPool))
			{
				objectPool = CreatePool(obj, prewarmLoad);
			}

			return (IObjectPool<T>)objectPool;
		}

		public IObjectPool<T> CreatePool<T>(T obj, int prewarmLoad) where T : PoolableObject<T>, new()
		{
			if (root == null) CreateRoot();

			var objType = typeof(T);
			var newObjectPool = new ObjectPool<T>(root, obj);
			newObjectPool.Init(prewarmLoad);
			pools.Add(objType, newObjectPool);

			return newObjectPool;
		}

		public void Dispose()
		{
			foreach (var kvp in pools)
			{
				IObjectPool pool = kvp.Value;
				pool.Dispose();
			}
			pools.Clear();
		}
	}
}
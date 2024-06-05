using Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.ObjectPool
{
	public class ObjectPoolService : SingletonInstance<ObjectPoolService>
	{
		private const string ROOT_NAME = "[Object Pool]";

		private readonly Dictionary<string, IObjectPool> pools = new();

		private Transform root;

		private void CreateRoot()
		{
			GameObject rootObject = new GameObject(ROOT_NAME);
			root = rootObject.transform;
			root.gameObject.SetActive(false);
			GameObject.DontDestroyOnLoad(rootObject);
		}

		public IObjectPool<T> GetOrCreatePool<T>(T obj, int prewarmLoad) where T : PoolableObject<T>, new()
		{
			var objType = typeof(T);
			string poolId = $"{objType} - {obj.gameObject.name}";

			if (!pools.TryGetValue(poolId, out var objectPool))
			{
				objectPool = CreatePool(obj, prewarmLoad);
			}

			return (IObjectPool<T>)objectPool;
		}

		public IObjectPool<T> CreatePool<T>(T obj, int prewarmLoad) where T : PoolableObject<T>, new()
		{
			if (root == null) CreateRoot();

			var objType = typeof(T);
			string poolId = $"{objType} - {obj.gameObject.name}";

			var newObjectPool = new ObjectPool<T>(poolId, root, obj);
			newObjectPool.Init(prewarmLoad);
			pools.Add(poolId, newObjectPool);

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
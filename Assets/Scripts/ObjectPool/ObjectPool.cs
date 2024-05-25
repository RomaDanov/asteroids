using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
	public class ObjectPool<T> : IObjectPool<T>, IDisposable where T : PoolableObject<T>
	{
		private T prefab;
		private Transform root;

		private Queue<T> availables = new Queue<T>();

		public ObjectPool(Transform mainRoot, T prefab)
		{
			this.prefab = prefab;

			var prefabType = typeof(T);
			CreateRoot(prefabType.Name, mainRoot);
		}

		public void Init(int prewarmLoad)
		{
			for (int i = 0; i < prewarmLoad; i++)
			{
				CreateObject();
			}
		}

		public T Get(Transform insertParent = null)
		{
			T available = null;
			if (availables.Count == 0)
			{
				CreateObject();
			}

			available = availables.Dequeue();
			available.transform.SetParent(insertParent, false);
			available.OnGet();

			return available;
		}

		public void Release(T obj)
		{
			availables.Enqueue(obj);
			obj.transform.SetParent(root);
			obj.OnRelease();
		}

		private void CreateRoot(string name, Transform mainRoot)
		{
			GameObject rootObject = new GameObject($"[{name}]");
			root = rootObject.transform;
			root.transform.SetParent(mainRoot);
			rootObject.SetActive(false);
		}

		private void CreateObject()
		{
			var newObject = UnityEngine.Object.Instantiate(prefab, root);
			newObject.Init(this);
			availables.Enqueue(newObject);
		}

		public void Dispose()
		{
			foreach (var obj in availables)
			{
				Release(obj);
			}
		}
	}
}
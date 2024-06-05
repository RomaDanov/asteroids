using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.ObjectPool
{
	public class ObjectPool<T> : IObjectPool<T>, IDisposable where T : PoolableObject<T>
	{
		private T prefab;
		private Transform root;

		private Queue<T> availables = new Queue<T>();

		public ObjectPool(string name, Transform mainRoot, T prefab)
		{
			this.prefab = prefab;
			CreateRoot(name, mainRoot);
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
			ResetTransform(available);

			if (insertParent == null)
			{
				SceneManager.MoveGameObjectToScene(available.gameObject, SceneManager.GetActiveScene());
			}

			available.OnGet();

			return available;
		}

		public void Release(T obj)
		{
			availables.Enqueue(obj);

			if (obj == null) return;

			obj.transform.SetParent(root);
			ResetTransform(obj);

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

		private void ResetTransform(T obj)
		{
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
using Architecture.ObjectPool;
using Architecture.WindowManagment;
using UnityEngine;

namespace UI.Windows
{
	public class MovablePanelsWindow : Window
	{
		public T Create<T>(UIMoveblePanel prefab, Transform owner) where T : UIMoveblePanel
		{
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefab, 10);
			UIMoveblePanel panel = pool.Get(transform);
			panel.transform.localScale = Vector3.one;
			panel.SetTarget(owner);
			return panel as T;
		}

		public T Create<T>(UIMoveblePanel prefab, Vector3 anchoredPosition) where T : UIMoveblePanel
		{
			var pool = ObjectPoolService.Instance.GetOrCreatePool(prefab, 10);
			UIMoveblePanel panel = pool.Get(transform);
			panel.transform.localScale = Vector3.one;
			panel.SetTarget(anchoredPosition);
			return panel as T;
		}
	}
}
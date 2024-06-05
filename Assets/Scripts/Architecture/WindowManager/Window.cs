using UnityEngine;

namespace Architecture.WindowManagment
{
	[DisallowMultipleComponent]
	public abstract class Window : MonoBehaviour
	{
		public bool IsInitialized { get; private set; }

		private void Awake()
		{
			Initialize();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Initialize()
		{
			if (IsInitialized) return;

			gameObject?.SetActive(false);
			WindowManager.Instance.AddWindow(this);

			IsInitialized = true;
			OnInitialized();
		}

		private void Dispose()
		{
			if (!IsInitialized) return;

			WindowManager.Instance.RemoveWindow(this);

			IsInitialized = false;
			OnDisposed();
		}

		public void Open()
		{
			gameObject?.SetActive(true);
			transform.SetAsLastSibling();
			OnWindowOpened();
		}

		public void Close()
		{
			gameObject?.SetActive(false);
			OnWindowClosed();
		}

		protected virtual void OnInitialized() { }
		protected virtual void OnWindowOpened() { }
		protected virtual void OnWindowClosed() { }
		protected virtual void OnDisposed() { }
	}
}
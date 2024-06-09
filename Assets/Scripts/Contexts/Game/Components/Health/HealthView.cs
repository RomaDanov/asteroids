using Architecture.ObjectPool;
using Architecture.WindowManagment;
using UI.Windows;
using UnityEngine;

namespace Contexts.Game.Components
{
	[RequireComponent(typeof(Health))]
	public class HealthView : MonoBehaviour
	{
		[SerializeField] private UIHealthBar healthBarPrefab;
		[SerializeField] private Explosion explosionFX;
		[SerializeField] private Health health;

		private UIHealthBar currentHealthBar;

		public void Configure()
		{
			if (!health.IsAlive) return;

			DisposeHealthBar();
			InitHealthBar();

			health.DamageTaken += OnDamageTaken;
			health.Died += OnDied;
		}

		private void OnDisable()
		{
			health.DamageTaken -= OnDamageTaken;
			health.Died -= OnDied;
		}

		private void InitHealthBar()
		{
			if (healthBarPrefab == null) return;
			currentHealthBar = WindowManager.Instance.GetWindow<MovablePanelsWindow>().Create<UIHealthBar>(healthBarPrefab, transform);
			OnDamageTaken(0);
		}

		private void DisposeHealthBar()
		{
			if (currentHealthBar == null) return;
			currentHealthBar.Pool.Release(currentHealthBar);
			currentHealthBar = null;
		}

		private void Explode(Vector3 position)
		{
			if (explosionFX == null) return;

			var explosionPool = ObjectPoolService.Instance.GetOrCreatePool(explosionFX, 10);
			Explosion explosion = explosionPool.Get();
			explosion.transform.position = position;
		}

		private void OnDamageTaken(float amount)
		{
			if (currentHealthBar == null) return;
			currentHealthBar.SetHealth(health.CurrentHealth / health.MaxHealth);
		}

		private void OnDied(Vector3 position)
		{
			Explode(position);
			DisposeHealthBar();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (health != null) return;
			health = GetComponent<Health>();
		}
#endif
	}
}
using System;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Destroyer : MonoBehaviour
	{
		public event Action Destroyed;

		[SerializeField] private float destroyAfter;

		private void OnEnable()
		{
			Invoke(nameof(InvokeDestroy), destroyAfter);
		}

		private void OnDisable()
		{
			CancelInvoke(nameof(InvokeDestroy));
		}

		private void InvokeDestroy()
		{
			if (Destroyed == null)
			{
				Destroy(gameObject);
			}
			else
			{
				Destroyed.Invoke();
			}
		}
	}
}
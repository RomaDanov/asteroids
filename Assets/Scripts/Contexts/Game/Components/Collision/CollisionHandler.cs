using System;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Collision
{
	public abstract class CollisionHandler : MonoBehaviour
	{
		public event Action<RaycastHit2D> CollisionStart;
		public event Action<RaycastHit2D> Collising;
		public event Action<RaycastHit2D> CollisionEnd;

		[SerializeField] protected LayerMask layerMask;
		[SerializeField] private float checkingInterval = 0.01f;

#if UNITY_EDITOR
		[SerializeField] protected Color color;
#endif

		private List<RaycastHit2D> currentCollisions = new();
		private List<RaycastHit2D> newCollisions = new();
		private float currentTime;

		public abstract RaycastHit2D[] CastAll();

		public void Configure(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			currentTime = checkingInterval;
			newCollisions = new();
		}

		public void FixedUpdate()
		{
			if (currentTime <= 0)
			{
				UpdateCollisionEnter(newCollisions);
				UpdateCollisionExit(newCollisions);

				UpdateCollision();

				currentTime = checkingInterval;

				IsCollised(out newCollisions);
			}
			else
			{
				currentTime -= Time.fixedDeltaTime;
			}
		}

		private bool IsCollised(out List<RaycastHit2D> others)
		{
			bool success = false;
			others = new();
			RaycastHit2D[] hitInfo = CastAll();
			if (hitInfo != null)
			{
				for (int i = 0; i < hitInfo.Length; i++)
				{
					RaycastHit2D other = hitInfo[i];

					if (other.transform == null) continue;
					if (other.transform.gameObject == gameObject) continue;
					if (others.Find(x => x.transform == other.transform) != default) continue;

					others.Add(other);
					success = true;
				}
			}
			return success;
		}

		private void UpdateCollisionEnter(List<RaycastHit2D> others)
		{
			for (int i = 0; i < others.Count; i++)
			{
				RaycastHit2D other = others[i];
				if (currentCollisions.Find(x => x.transform == other.transform) != default) continue;

				CollisionStart?.Invoke(other);
				currentCollisions.Add(other);
			}
		}

		private void UpdateCollision()
		{
			for (int i = 0; i < currentCollisions.Count; i++)
			{
				RaycastHit2D current = currentCollisions[i];
				Collising?.Invoke(current);
			}
		}

		private void UpdateCollisionExit(List<RaycastHit2D> others)
		{
			for (int i = 0; i < currentCollisions.Count; i++)
			{
				RaycastHit2D current = currentCollisions[i];
				if (others.Find(x => x.transform == current.transform) != default) continue;

				CollisionEnd?.Invoke(current);
				currentCollisions.RemoveAt(i);
				i--;
			}
		}
	}
}
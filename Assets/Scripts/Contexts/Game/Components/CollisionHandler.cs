using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	[Serializable]
	private enum ColliderType
	{
		Circle,
		Box
	}

	public event Action<RaycastHit2D> CollisionStart;
	public event Action<RaycastHit2D> Collising;
	public event Action<RaycastHit2D> CollisionEnd;

	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float size;
	[SerializeField] private float checkingInterval = 0.01f;

	private List<RaycastHit2D> currentCollisions = new();
	private float currentTime;

	public void Configure(LayerMask layerMask)
	{
		this.layerMask = layerMask;
	}

	private void OnEnable()
	{
		currentTime = checkingInterval;
	}

	public void FixedUpdate()
	{
		if (currentTime <= 0)
		{
			if (IsCollised(out List<RaycastHit2D> newCollisions))
			{
				UpdateCollisionEnter(newCollisions);
				UpdateCollisionExit(newCollisions);

				UpdateCollision();

				currentTime = checkingInterval;
			}
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
		RaycastHit2D[] hitInfo = Physics2D.CircleCastAll(transform.position, size, Vector2.zero, 0, layerMask);
		if (hitInfo != null)
		{
			for (int i = 0; i < hitInfo.Length; i++)
			{
				RaycastHit2D other = hitInfo[i];

				if (other.transform.gameObject == gameObject) continue;
				if (others.Contains(other)) continue;

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
			if (currentCollisions.Contains(other)) continue;

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
			if (others.Contains(current)) continue;

			CollisionEnd?.Invoke(current);
			currentCollisions.RemoveAt(i);
			i--;
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (!enabled) return;
		Gizmos.DrawWireSphere(transform.position, size);
	}
#endif
}

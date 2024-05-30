using Contexts.Game.Components;
using Contexts.Game.Components.Weapons;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileDamageApplier : MonoBehaviour
{
	public event Action<List<IDamageable>> Damaged;

	[SerializeField] private float defaultRangeSize;
	[SerializeField] private Destroyer destroyer;

	private DamageInfo damageInfo;

	public void Configure(DamageInfo damageInfo)
	{
		this.damageInfo = damageInfo;
		destroyer.Destroyed += OnDestroyer;
	}

	private void Update()
	{
		if (IsTargetCollision(damageInfo.TargetLayers, defaultRangeSize, out var targets))
		{
			if (damageInfo.Range <= 0)
			{
				ApplyDamage(targets);
			}
			else
			{
				TryApplyRangeDamage();
			}
		}
	}

	private void OnDisable()
	{
		destroyer.Destroyed -= OnDestroyer;
	}

	private bool IsTargetCollision(LayerMask targetLayers, float size, out List<IDamageable> targets)
	{
		bool success = false;
		targets = new();
		Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, size, targetLayers);
		if (hitInfo != null)
		{
			for (int i = 0; i < hitInfo.Length; i++)
			{
				IDamageable target = hitInfo[i].gameObject.GetComponent<IDamageable>();
				if (target == null)
				{
					target = hitInfo[i].transform.parent.GetComponent<IDamageable>();
				}

				if (target == null) continue;
				if (targets.Contains(target)) continue;

				targets.Add(target);
				success = true;
			}
		}
		return success;
	}

	private void ApplyDamage(List<IDamageable> targets)
	{
		for (int i = 0; i < targets.Count; i++)
		{
			IDamageable target = targets[i];
			target.TakeDamage(damageInfo.Damage);
		}
		Damaged?.Invoke(targets);
	}

	private void TryApplyRangeDamage()
	{
		if (IsTargetCollision(damageInfo.TargetLayers, damageInfo.Range, out var targets))
		{
			ApplyDamage(targets);
		}
	}

	private void OnDestroyer()
	{
		destroyer.Destroyed -= OnDestroyer;
		TryApplyRangeDamage();
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, defaultRangeSize);
	}
#endif
}

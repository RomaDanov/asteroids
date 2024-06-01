using Contexts.Game.Components.Collision;
using Contexts.Game.Components.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamageApplier : MonoBehaviour
{
	public event Action<List<IDamageable>> DamageApplied;

	[SerializeField] private CollisionHandler collisionHandler;

	private DamageInfo damageInfo;

	public void Configure(DamageInfo damageInfo)
	{
		this.damageInfo = damageInfo;
	}

	private void OnEnable()
	{
		collisionHandler.CollisionStart += OnCollisionStart;
	}

	private void OnDisable()
	{
		collisionHandler.CollisionStart -= OnCollisionStart;
	}

	private void OnCollisionStart(RaycastHit2D other)
	{
		if (other.transform.gameObject == null) return;

		IDamageable target = other.transform.GetComponent<IDamageable>();
		if (target == null) return;

		List<IDamageable> damaged = new();
		if (damageInfo.Range <= 0)
		{
			ApplyDamage(target);
			damaged.Add(target);
		}
		else
		{
			List<IDamageable> targets = TryApplyRangeDamage();
			damaged.AddRange(targets);
		}

		if (damaged.Count > 0)
		{
			DamageApplied?.Invoke(damaged);
		}
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
				if (target == null) continue;
				if (targets.Contains(target)) continue;

				targets.Add(target);
				success = true;
			}
		}
		return success;
	}

	private void ApplyDamage(IDamageable target)
	{
		target.TakeDamage(damageInfo.Damage);
	}

	private List<IDamageable> TryApplyRangeDamage()
	{
		if (IsTargetCollision(damageInfo.TargetLayers, damageInfo.Range, out var targets))
		{
			for (int i = 0; i < targets.Count; i++)
			{
				IDamageable target = targets[i];

				ApplyDamage(target);
			}
			return targets;
		}
		return new();
	}
}

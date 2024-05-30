using Configs.Weapons;
using Contexts.Game.Components.Movement;
using UnityEngine;

namespace Contexts.Game.Components.Weapons
{
	[RequireComponent(typeof(IMovable))]
	public class ProjectileMovement : MonoBehaviour
	{
		private IMovable movable;

		public void Configure(ProjectileStats stats, Vector2 pushDirection)
		{
			if (movable == null) movable = GetComponent<IMovable>();

			LookAt(pushDirection);
			Push(transform.up * stats.Speed);
		}

		public void ForceStop()
		{
			movable.Velocity = Vector2.zero;
		}

		private void LookAt(Vector3 direction)
		{
			transform.up = direction;
		}

		private void Push(Vector3 force)
		{
			movable.Velocity = force;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (movable != null) return;
			movable = GetComponent<IMovable>();
		}
#endif
	}
}
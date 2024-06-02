using UnityEngine;

namespace Contexts.Game.Components.Enemy
{
	public class EnemyAttack : MonoBehaviour
	{
		[SerializeField] private Equipments equipments;

		private Transform target;
		private float attackRange;

		public void Configure(float attackRange)
		{
			this.attackRange = attackRange;

			if (target == null)
			{
				Player.Player player = FindObjectOfType<Player.Player>();
				if (player != null)
				{
					target = player.transform;
				}
			}
		}
		public void Update()
		{
			if (target == null) return;

			if (Vector2.Distance(transform.position, target.position) <= attackRange)
			{
				PerfomAttack();
			}
		}

		private void PerfomAttack()
		{
			Weapon weapon = equipments.GetWeapon(0);
			if (weapon == null) return;
			weapon.TryAttack();
		}
	}
}
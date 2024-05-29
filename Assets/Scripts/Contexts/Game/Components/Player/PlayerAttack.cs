using Inputs;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class PlayerAttack : MonoBehaviour
	{
		[SerializeField] private PlayerEquipments equipments;

		public void Update()
		{
			IInput input = InputManager.Instance.Input;
			if (input == null) return;

			bool isFire = input.GetFire();
			bool isAlternativeFire = input.GetAlternativeFire();

			if (isFire) PerfomAttack(0);
			if (isAlternativeFire) PerfomAttack(1);
		}

		private void PerfomAttack(int slotId)
		{
			Weapon weapon = equipments.GetWeapon(slotId);
			if (weapon == null) return;
			weapon.TryAttack();
		}
	}
}
using Inputs;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private PlayerEquipments equipments;

	public void Update()
	{
		IInput input = InputManager.Instance.Input;
		if (input == null) return;

		bool isFire = input.GetFire();
		bool isAlternativeFire = input.GetAlternativeFire();
	}
}

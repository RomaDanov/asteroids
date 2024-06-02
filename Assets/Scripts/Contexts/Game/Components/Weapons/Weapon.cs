using Configs.Weapons;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public abstract void Configure(WeaponConfig config, LayerMask target);
	public abstract bool TryAttack();
}

using Configs.Weapons;
using ObjectPool;
using UnityEngine;

public abstract class Weapon : PoolableObject<Weapon>
{
	public abstract void Configure(WeaponConfig config, LayerMask target);
	public abstract bool TryAttack();
}

using Configs.Weapons;
using ObjectPool;

public abstract class Weapon : PoolableObject<Weapon>
{
	public abstract void Configure(WeaponConfig config);
	public abstract bool TryAttack();
}

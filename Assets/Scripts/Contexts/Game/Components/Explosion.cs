using Architecture.ObjectPool;

namespace Contexts.Game.Components
{
	public class Explosion : PoolableObject<Explosion>
	{
		public void Release()
		{
			Pool.Release(this);
		}
	}
}
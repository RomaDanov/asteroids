namespace ObjectPool
{
	public interface IPoolableObject
	{
		void OnGet();
		void OnRelease();
	}
}
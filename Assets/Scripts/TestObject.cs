using ObjectPool;

public class TestObject : PoolableObject<TestObject>
{
	public void Release()
	{
		Pool.Release(this);
	}

	public override void OnGet()
	{
		Invoke(nameof(Release), UnityEngine.Random.Range(0.1f, 1f));
	}
}

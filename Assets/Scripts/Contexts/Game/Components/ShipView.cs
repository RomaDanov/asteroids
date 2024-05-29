using ObjectPool;

public class ShipView : PoolableObject<ShipView>
{
	public string Id { get; private set; }

	public void Configure(string id)
	{
		Id = id;
	}
}

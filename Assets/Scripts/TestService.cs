using ServiceLocator;
using UnityEngine;

public class TestService : ServiceInstance
{
	public override void PreLoad()
	{
		Debug.Log("Preload");
	}

	public override void Load()
	{
		Debug.Log("Load");
	}

	public override void PostLoad()
	{
		Debug.Log("PostLoad");
	}

	public override void Dispose()
	{
		Debug.Log("Dispose");
	}
}

using Contexts.Game.Components.Fence;
using UnityEngine;

public class GameObjectDeadzoneVisitor : MonoBehaviour, IDeadzoneVisitor
{
	public void Visit()
	{
		Destroy(gameObject);
	}
}

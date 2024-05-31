using Contexts.Game.Components.Zone;
using UnityEngine;

public class GameObjectDeadzoneVisitor : MonoBehaviour, IDeadzoneVisitor
{
	public void Visit()
	{
		Destroy(gameObject);
	}
}

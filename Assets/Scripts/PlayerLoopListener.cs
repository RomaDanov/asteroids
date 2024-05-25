using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

public class PlayerLoopListener
{
	private PlayerLoopSystem.UpdateFunction onUpdate;

	public PlayerLoopListener(PlayerLoopSystem.UpdateFunction onUpdate)
	{
		this.onUpdate = onUpdate;
	}

	public void Subscribe()
	{
		PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
		for (var i = 0; i < currentPlayerLoop.subSystemList.Length; ++i)
		{
			if (currentPlayerLoop.subSystemList[i].type == typeof(Update))
			{
				currentPlayerLoop.subSystemList[i].updateDelegate += onUpdate;
			}
		}
		PlayerLoop.SetPlayerLoop(currentPlayerLoop);
	}

	public void Unsubscribe()
	{
		PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
		for (var i = 0; i < currentPlayerLoop.subSystemList.Length; ++i)
		{
			if (currentPlayerLoop.subSystemList[i].type == typeof(Update))
			{
				currentPlayerLoop.subSystemList[i].updateDelegate -= onUpdate;
			}
		}
		PlayerLoop.SetPlayerLoop(currentPlayerLoop);
	}
}

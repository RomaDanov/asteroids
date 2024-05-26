using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

public static class PlayerLoopUtils
{
	public static void RegisterUpdateFunction(PlayerLoopSystem.UpdateFunction updateFunction)
	{
		PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
		for (var i = 0; i < currentPlayerLoop.subSystemList.Length; ++i)
		{
			if (currentPlayerLoop.subSystemList[i].type != typeof(Update)) continue;

			currentPlayerLoop.subSystemList[i].updateDelegate += updateFunction;
		}
		PlayerLoop.SetPlayerLoop(currentPlayerLoop);
	}

	public static void UnregisterUpdateFunction(PlayerLoopSystem.UpdateFunction updateFunction)
	{
		PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
		for (var i = 0; i < currentPlayerLoop.subSystemList.Length; ++i)
		{
			if (currentPlayerLoop.subSystemList[i].type != typeof(Update)) continue;

			currentPlayerLoop.subSystemList[i].updateDelegate -= updateFunction;
		}
		PlayerLoop.SetPlayerLoop(currentPlayerLoop);
	}
}

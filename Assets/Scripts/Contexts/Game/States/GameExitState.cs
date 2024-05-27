using StateMachine;
using UnityEditor;

namespace Contexts.Game.States
{
	public class GameExitState : State
	{
		internal override void Enter()
		{
#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}
	}
}
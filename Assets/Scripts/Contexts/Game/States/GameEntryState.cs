using StateMachine;

namespace Contexts.Game.States
{
	public class GameEntryState : State
	{
		internal override void Enter()
		{
			Finish();
		}
	}
}